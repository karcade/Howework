package main

import (
	"context"
	"fmt"
	"log"
	"net/http"
	"time"

	"github.com/gin-gonic/gin"
	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"

	_ "example.com/task-handbook/docs"
	swaggerFiles "github.com/swaggo/files"
	ginSwagger "github.com/swaggo/gin-swagger"
)

type task struct {
	ID   string    `json:"id"`
	Task string    `json:"task"`
	Done bool      `json:"done"`
	Due  time.Time `json:"due"`
}

var tasks = []task{
	{ID: "1", Task: "parse route header", Done: false, Due: time.Date(2022, 11, 1, 0, 0, 0, 0, time.UTC)},
	{ID: "2", Task: "fix bugs", Done: false, Due: time.Date(2022, 11, 2, 0, 0, 0, 0, time.UTC)},
	{ID: "3", Task: "add search function", Done: false, Due: time.Date(2022, 11, 3, 0, 0, 0, 0, time.UTC)},
	{ID: "4", Task: "test fuction", Done: false, Due: time.Date(2022, 11, 4, 0, 0, 0, 0, time.UTC)},
}

func init() {
	collection = getCollection()
	_, err := OpenLogFile()
	log.Println(err)
}

// @title Task Swagger API
// @version 1.0
// @description Swagger API for Golang Project

// @host localhost:8080
// @BasePath /

//@securityDefinitions.apikey ApiKeyAuth
//@in header
//@name Authorization
func main() {
	router := gin.Default()

	authorized := router.Group("/", someBasicAuth())

	router.POST("/tasks", postTasks)
	router.GET("/tasks", getTasks)
	router.GET("/tasks/:id", otherBasicAuth, getTaskByID)
	router.PUT("/tasks/:id", updateTaskByID)
	authorized.DELETE("/tasks/:id", deleteTaskByID)
	router.GET("/swagger/*any", ginSwagger.WrapHandler(swaggerFiles.Handler))

	router.Run("localhost:8080")
}

var collection *mongo.Collection
var userCollection *mongo.Collection

// @Summary POST
// @Tags requests
// @Description Post task
// @Accept json
// @Produce json
// @Param input body task true "new task"
// @Success 200 {integer} integer 1
// @Router /tasks [post]
func postTasks(c *gin.Context) {
	log.Println("POST tasks")

	var newTask task

	if err := c.BindJSON(&newTask); err != nil {
		c.IndentedJSON(http.StatusBadRequest, gin.H{"message": "bad request"})
		return
	}

	res, err := collection.InsertOne(context.Background(), newTask)
	if err != nil {
		log.Fatal(err)
	}

	log.Printf("object %v\n", res)
	tasks = append(tasks, newTask)
	c.IndentedJSON(http.StatusOK, gin.H{"message": "task was added"})
}

// @Summary GET
// @Tags requests
// @Description Get tasks
// @Accept json
// @Produce json
// @Success 200 {integer} integer 1
// @Router /tasks [get]
func getTasks(c *gin.Context) {
	cur, err := collection.Find(context.Background(), bson.D{})
	log.Println("GET tasks: ")
	if err != nil {
		log.Fatal(err)
		log.Println(err)
	}
	defer cur.Close(context.Background())

	var foundTasks []*task
	for cur.Next(context.Background()) {
		result := task{}
		err := cur.Decode(&result)
		log.Println(result)
		if err != nil {
			log.Fatal(err)
		}
		raw := cur.Current
		fmt.Printf("Raw result entry: %v\n", raw)
		foundTasks = append(foundTasks, &result)
	}
	log.Println(foundTasks)
	c.IndentedJSON(http.StatusOK, foundTasks)
}

// @Summary GET BY ID
// @Tags requests
// @Description Get tasks
// @Accept json
// @Produce json
// @Param id path integer true "id"
// @Success 200 {integer} integer 1
// @Router /tasks/{id} [get]
// @Security ApiKeyAuth
func getTaskByID(c *gin.Context) {
	id := c.Param("id")
	log.Println("GET task by id=", id)
	var result task
	err := collection.FindOne(
		context.Background(),
		bson.D{{Key: "id", Value: id}},
	).Decode(&result)

	if err != nil {
		log.Print("task do not found")
		if err == mongo.ErrNoDocuments {
			c.IndentedJSON(http.StatusNotFound, gin.H{"message": "task do not found"})
			return
		}
	}
	c.IndentedJSON(http.StatusOK, result)
	log.Print("Task was found:", result)
}

// @Summary UPDATE
// @Tags requests
// @Description Update task
// @Accept json
// @Produce json
// @Param id path integer true "id"
// @Param input body task true "updated task"
// @Success 200 {integer} integer 1
// @Router /tasks/{id} [put]
func updateTaskByID(c *gin.Context) {
	id := c.Param("id")
	log.Println("UPDATE task by id", id)

	var updatedTask task

	if err := c.BindJSON(&updatedTask); err != nil {
		c.IndentedJSON(http.StatusBadRequest, gin.H{"message": "bad request"})
		log.Fatal(err)
	}

	opts := options.FindOneAndUpdate().SetUpsert(true)
	filter := bson.D{{Key: "id", Value: updatedTask.ID}}
	update := bson.D{{"$set", bson.D{{
		Key: "task", Value: updatedTask.Task}, {
		Key: "done", Value: updatedTask.Done}, {
		Key: "due", Value: updatedTask.Due}}}}

	var updatedDocument bson.M
	err := collection.FindOneAndUpdate(
		context.TODO(),
		filter,
		update,
		opts,
	).Decode(&updatedDocument)

	if err != nil {
		if err == mongo.ErrNoDocuments {
			return
		}
		log.Fatal(err)
		c.IndentedJSON(http.StatusOK, updatedTask)
	}
	c.IndentedJSON(http.StatusOK, gin.H{"message": "task was updated"})
	log.Println("Task was updated: ", updatedTask)
}

// @Summary DELETE
// @Tags requests
// @Description Delete task
// @Accept json
// @Produce json
// @Param id path integer true "id"
// @Success 200 {integer} integer 1
// @Security ApiKeyAuth
// @Router /tasks/{id} [delete]
func deleteTaskByID(c *gin.Context) {
	id := c.Param("id")
	log.Println("DELETE task by id", id)
	opts := options.Delete().SetCollation(&options.Collation{
		Locale:    "en_US",
		Strength:  1,
		CaseLevel: false,
	})
	_, err := collection.DeleteOne(context.Background(), bson.M{"id": id}, opts)
	if err != nil {
		log.Printf("Error: %v\n", err)
		c.IndentedJSON(http.StatusInternalServerError, gin.H{
			"status":  http.StatusInternalServerError,
			"message": "something went wrong",
		})
	}
	c.IndentedJSON(http.StatusOK, gin.H{
		"status":  http.StatusOK,
		"message": "task was deleted",
	})
	log.Println("task was deleted")
}

package main

import (
	"log"
	"net/http"

	"github.com/gin-gonic/gin"
	"golang.org/x/crypto/bcrypt"
)

type User struct {
	Username string `json:"username"`
	Password string `json:"password"`
}

func (user *User) HashPassword(password string) error {
	bytes, err := bcrypt.GenerateFromPassword([]byte(password), 14)
	if err != nil {
		return err
	}
	user.Password = string(bytes)
	return nil
}

func (user *User) CheckPassword(providedPassword string) error {
	err := bcrypt.CompareHashAndPassword([]byte(user.Password), []byte(providedPassword))
	if err != nil {
		return err
	}
	return nil
}

var users = []User{}

func RegisterUser(context *gin.Context) {
	var user User

	if err := context.ShouldBindJSON(&user); err != nil {
		context.JSON(http.StatusBadRequest, gin.H{"error": "err.Error()"})
		context.Abort()
		return
	}

	if err := user.HashPassword(user.Password); err != nil {
		context.JSON(http.StatusInternalServerError, gin.H{"error": ""})
		context.Abort()
		return
	}

	result, err := userCollection.InsertOne(context, &user)
	if err != nil {
		log.Fatal(err)
		context.JSON(http.StatusInternalServerError, gin.H{"message": "Internal Server Error"})
		context.Abort()
		return
	}

	log.Printf("object %v\n", result)

	users = append(users, user)
	context.JSON(http.StatusCreated, gin.H{"username": user.Username, "password": user.Password})
}

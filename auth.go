package main

import (
	"context"
	"encoding/base64"
	"fmt"
	"log"
	"strings"

	"github.com/gin-gonic/gin"
	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo"
)

func someBasicAuth() gin.HandlerFunc {
	return func(c *gin.Context) {
		fmt.Printf(c.Request.Header.Get("Authorization"))
		auth := strings.SplitN(c.Request.Header.Get("Authorization"), " ", 2)

		if len(auth) != 2 || auth[0] != "Basic" {
			respondWithError(401, "Unauthorized", c)
			return
		}

		payload, _ := base64.StdEncoding.DecodeString(auth[1])
		pair := strings.SplitN(string(payload), ":", 2)

		if len(pair) != 2 || !authenticateUser(pair[0], pair[1]) {
			respondWithError(401, "Unauthorized", c)
			return
		}
		c.Next()
	}
}

func otherBasicAuth(c *gin.Context) {

	auth := strings.SplitN(c.Request.Header.Get("Authorization"), " ", 2)

	if len(auth) != 2 { //|| auth[0] != "Basic"
		respondWithError(401, "Unauthorized", c)
		return
	}

	payload, _ := base64.StdEncoding.DecodeString(auth[1])
	pair := strings.SplitN(string(payload), ":", 2)

	if len(pair) != 2 || !authenticateUser(pair[0], pair[1]) {
		respondWithError(401, "Unauthorized", c)
		return
	}
	c.Next()
}

func respondWithError(code int, message string, c *gin.Context) {
	resp := map[string]string{"error": message}
	c.JSON(code, resp)
	c.Abort()
}

func authenticateUser(user, password string) bool {
	userCollection = getUsersCollection()

	var result User
	err := userCollection.FindOne(
		context.Background(),
		bson.D{{Key: "password", Value: password}, {Key: "username", Value: user}},
	).Decode(&result)

	if err != nil {
		log.Print("user do not found")
		if err == mongo.ErrNoDocuments {
			return false
		}
	}

	return true
}

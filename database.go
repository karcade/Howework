package main

import (
	"context"
	"encoding/json"
	"fmt"
	"os"
	"time"

	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

type Configuration struct {
	URI             string
	Database        string
	Collection      string
	UsersCollection string
}

func getConfigeration() Configuration {
	file, err := os.ReadFile("conf.json")

	if err != nil {
		fmt.Println(err)
	}

	configuration := Configuration{}

	err = json.Unmarshal(file, &configuration)
	if err != nil {
		fmt.Println(err)
	}
	return configuration
}

func openConnection() (*mongo.Client, error) {
	file, err := os.ReadFile("conf.json")

	if err != nil {
		fmt.Println(err)
	}

	configuration := Configuration{}

	err = json.Unmarshal(file, &configuration)
	if err != nil {
		fmt.Println(err)
	}

	fmt.Println("URI:", configuration.URI)
	fmt.Println("DB:", configuration.Database)

	ctx, cancel := context.WithTimeout(context.Background(), 20*time.Second)
	defer cancel()
	client, err := mongo.Connect(ctx, options.Client().ApplyURI(configuration.URI))
	if err != nil {
		fmt.Println(err)
	}

	fmt.Printf("Client value %v\n", client)

	return client, err
}

func getCollection() *mongo.Collection {
	client, _ := openConnection()
	collection := client.Database(getConfigeration().Database).Collection(getConfigeration().Collection)
	fmt.Printf("Collection value %v\n", collection)
	return collection
}

func getUsersCollection() *mongo.Collection {
	client, _ := openConnection()
	usersCollection := client.Database(getConfigeration().Database).Collection(getConfigeration().UsersCollection)
	fmt.Printf("Collection value %v\n", usersCollection)
	return usersCollection
}

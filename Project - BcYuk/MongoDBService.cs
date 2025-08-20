using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

public static class MongoDBService
{
    private static readonly string connectionString = "mongodb+srv://mongoBCYuK:mongoDbBcYuk@bcyuk.cjsqfcp.mongodb.net/?retryWrites=true&w=majority&appName=BCYuk";
    private static readonly string databaseName = "BCYuk";

    private static IMongoDatabase GetDatabase()
    {
        var client = new MongoClient(connectionString);
        return client.GetDatabase(databaseName);
    }

    public static IMongoCollection<BsonDocument> GetCollection(string name)
    {
        return GetDatabase().GetCollection<BsonDocument>(name);
    }

    // 👉 GET ALL
    public static List<BsonDocument> GetAll(string collectionName)
    {
        var collection = GetCollection(collectionName);
        return collection.Find(new BsonDocument()).ToList();
    }

    // 👉 GET by FILTER
    public static List<BsonDocument> GetByFilter(
     string collectionName,
     FilterDefinition<BsonDocument> filter,
     SortDefinition<BsonDocument> sort = null)
    {
        var collection = GetCollection(collectionName);
        var find = collection.Find(filter);

        if (sort != null)
            find = find.Sort(sort);

        return find.ToList();
    }


    // 👉 INSERT ONE
    public static void Insert(string collectionName, BsonDocument document)
    {
        var collection = GetCollection(collectionName);
        collection.InsertOne(document);
    }

    // 👉 INSERT MANY
    public static void InsertMany(string collectionName, List<BsonDocument> documents)
    {
        var collection = GetCollection(collectionName);
        collection.InsertMany(documents);
    }

    // 👉 UPDATE
    public static void Update(string collectionName, FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
    {
        var collection = GetCollection(collectionName);
        collection.UpdateOne(filter, update);
    }

    // 👉 DELETE
    public static void Delete(string collectionName, FilterDefinition<BsonDocument> filter)
    {
        var collection = GetCollection(collectionName);
        collection.DeleteOne(filter);
    }
}

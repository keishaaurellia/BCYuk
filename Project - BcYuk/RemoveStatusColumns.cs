using MongoDB.Bson;
using MongoDB.Driver;
using System;

public static class MongoCleanupService
{
    private static readonly string mongoConnectionString = "mongodb+srv://mongoBCYuK:mongoDbBcYuk@bcyuk.cjsqfcp.mongodb.net/?retryWrites=true&w=majority&appName=BCYuk";
    private static readonly string mongoDatabaseName = "BCYuk";

    public static void RemoveStatusColumns()
    {
        var client = new MongoClient(mongoConnectionString);
        var db = client.GetDatabase(mongoDatabaseName);

        // 🧽 Hapus StatusId dari Order
        var orderCollection = db.GetCollection<BsonDocument>("UserAuth");
        var updateOrder = Builders<BsonDocument>.Update.Unset("Email");
        orderCollection.UpdateMany(FilterDefinition<BsonDocument>.Empty, updateOrder);
        Console.WriteLine("✅ Kolom 'Email' di 'Order' berhasil dihapus.");
    }
}

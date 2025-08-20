using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

public static class DuplicateCleanerAll
{
    private static readonly string mongoConnectionString = "mongodb+srv://mongoBCYuK:mongoDbBcYuk@bcyuk.cjsqfcp.mongodb.net/?retryWrites=true&w=majority&appName=BCYuk";
    private static readonly string mongoDatabaseName = "BCYuk";

    private static readonly string[] collectionsToClean = new[]
    {
        "Order",
        "OrderDetail",
        "Product",
        "Category",
        "Class",
        "Receipt",
        "Payment",
        "PaymentMethod",
        "Status",
        "SalesReport",
        "PickupSchedule",
        "UserAuth",
        "Cart"
    };

    public static void CleanAllDuplicates()
    {
        var client = new MongoClient(mongoConnectionString);
        var db = client.GetDatabase(mongoDatabaseName);

        foreach (var collectionName in collectionsToClean)
        {
            var collection = db.GetCollection<BsonDocument>(collectionName);
            var allDocs = collection.Find(new BsonDocument()).ToList();

            var grouped = allDocs
                .Where(d => d.Contains("ID"))
                .GroupBy(d => d["ID"].ToString())
                .Where(g => g.Count() > 1);

            int deletedCount = 0;

            foreach (var group in grouped)
            {
                var docs = group.ToList();
                var keepOne = docs.First();
                var toDelete = docs.Skip(1);

                foreach (var doc in toDelete)
                {
                    var id = doc["_id"];
                    collection.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", id));
                    deletedCount++;
                }
            }

            Console.WriteLine($"✅ {collectionName}: {deletedCount} duplicate(s) deleted.");
        }

        Console.WriteLine("🎉 Semua duplikat selesai dibersihkan.");
    }
}

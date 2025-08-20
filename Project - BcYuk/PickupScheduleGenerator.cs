using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

public static class PickupScheduleGenerator
{
    private static readonly string mongoConnectionString = "mongodb+srv://mongoBCYuK:mongoDbBcYuk@bcyuk.cjsqfcp.mongodb.net/?retryWrites=true&w=majority&appName=BCYuk";
    private static readonly string mongoDatabaseName = "BCYuk";


    private static TimeSpan WibToUtc(TimeSpan wib)
    {
        return wib - new TimeSpan(7, 0, 0); // Kurangi 7 jam
    }

    public static void GenerateScheduleForToday()
    {
        var client = new MongoClient(mongoConnectionString);
        var db = client.GetDatabase(mongoDatabaseName);
        var collection = db.GetCollection<BsonDocument>("PickupSchedule");

        var today = DateTime.UtcNow.Date; // ⬅️ GANTI INI, biar sama kayak query-mu

        // 🧹 Hapus jadwal pickup hari ini terlebih dahulu
        var deleteFilter = Builders<BsonDocument>.Filter.Eq("PickupDate", today);
        collection.DeleteMany(deleteFilter); // 💥 ini baris pentingnya!

        var start = today;
        var end = today.AddDays(1);

        var filter = Builders<BsonDocument>.Filter.Gte("PickupDate", start) &
                     Builders<BsonDocument>.Filter.Lt("PickupDate", end);

        var existing = collection.Find(filter).ToList();

        if (existing.Any())
        {
            Console.WriteLine("⏩ Jadwal hari ini sudah ada.");
            return;
        }


        // 🕒 Generate jadwal default (dalam menit biar mudah di-filter)
        var pickupTimes = new List<(TimeSpan start, TimeSpan end, string description)>
        {
           (WibToUtc(new TimeSpan(7, 0, 0)), WibToUtc(new TimeSpan(9, 0, 0)), "Ambil Pagi"),
           (WibToUtc(new TimeSpan(12, 0, 0)), WibToUtc(new TimeSpan(13, 0, 0)), "Ambil Siang"),
           (WibToUtc(new TimeSpan(15, 0, 0)), WibToUtc(new TimeSpan(23, 0, 0)), "Ambil Sore")

            //(new TimeSpan(14, 0, 0), new TimeSpan(18, 0, 0), "Ambil Pagi"),
            //(new TimeSpan(24, 0, 0), new TimeSpan(26, 0, 0), "Ambil Siang"),
            //(new TimeSpan(30, 0, 0), new TimeSpan(46, 0, 0), "Ambil Sore")
        };

        var docs = pickupTimes.Select((t, i) => new BsonDocument
        {
            { "ID", int.Parse(today.ToString("yyyyMMdd")) * 10 + i },
            { "PickupDate", today },
            { "PickupTime", t.start.ToString(@"hh\:mm\:ss") }, // string: 07:00:00
            { "PickupEndTime", t.end.ToString(@"hh\:mm\:ss") },
            { "PickupTimeMinutes", (int)t.start.TotalMinutes },
            { "PickupEndTimeMinutes", (int)t.end.TotalMinutes },
            { "Description", t.description }
        }).ToList();

        collection.InsertMany(docs);
        Console.WriteLine("✅ Pickup Schedule untuk hari ini berhasil dibuat.");
    }
}

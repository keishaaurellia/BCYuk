using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public static class MongoDBMigration
{
    private static readonly string mongoConnectionString = "mongodb+srv://mongoBCYuK:mongoDbBcYuk@bcyuk.cjsqfcp.mongodb.net/?retryWrites=true&w=majority&appName=BCYuk";
    private static readonly string mongoDatabaseName = "BCYuk";
    private static readonly string sqlConnectionString = "Server=localhost;Database=BCYuks;Trusted_Connection=True;";
    

    public static void MigrateAll()
    {
        MigrateCategoryData();
        MigrateClassData();
        MigrateProductData();
        MigrateOrderData();
        MigrateOrderDetailData();
        MigrateReceiptData();
        MigratePaymentMethodData();
        MigratePaymentData();
        MigrateStatusData();
        MigrateSalesReportData();
        MigratePickupScheduleData();
        MigrateUserAuthData();
        MigrateCartData();
        Console.WriteLine("✅ Semua data berhasil dimigrasikan ke MongoDB!");
    }

    private static IMongoCollection<BsonDocument> GetMongoCollection(string name)
    {
        var client = new MongoClient(mongoConnectionString);
        var db = client.GetDatabase(mongoDatabaseName);
        return db.GetCollection<BsonDocument>(name);
    }

    private static void MigrateCategoryData()
    {
        var collection = GetMongoCollection("Category");

        using (var sql = new SqlConnection(sqlConnectionString))
        {
            sql.Open();
            using (var cmd = new SqlCommand("SELECT * FROM Category", sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var docs = new List<BsonDocument>();
                    while (reader.Read())
                    {
                        docs.Add(new BsonDocument
                    {
                        { "ID", reader.GetInt32(0) },
                        { "CategoryName", reader.GetString(1) }
                    });
                    }
                    collection.InsertMany(docs);
                }
            }
        }
    }


    private static void MigrateClassData()
    {
        var collection = GetMongoCollection("Class");

        using (var sql = new SqlConnection(sqlConnectionString))
        {
            sql.Open();
            using (var cmd = new SqlCommand("SELECT * FROM Class", sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var docs = new List<BsonDocument>();
                    while (reader.Read())
                    {
                        docs.Add(new BsonDocument
                    {
                        { "ID", reader.GetInt32(0) },
                        { "ClassName", reader.GetString(1) }
                    });
                    }

                    collection.InsertMany(docs);
                }
            }
        }
    }


    private static void MigrateProductData()
    {
        var collection = GetMongoCollection("Product");

        using (var sql = new SqlConnection(sqlConnectionString))
        {
            sql.Open();
            using (var cmd = new SqlCommand("SELECT * FROM Product", sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var docs = new List<BsonDocument>();
                    while (reader.Read())
                    {
                        docs.Add(new BsonDocument
                    {
                        { "ID", reader.GetInt32(0) },
                        { "ProductName", reader.GetString(1) },
                        { "CategoryID", reader.GetInt32(2) },
                        { "Stock", reader.GetInt32(3) },
                        { "ImageURL", reader.IsDBNull(4) ? "" : reader.GetString(4) },
                        { "Price", reader.IsDBNull(5) ? 0 : reader.GetDecimal(5) },
                        { "HPP", reader.IsDBNull(6) ? 0 : reader.GetDecimal(6) },
                        { "Margin", reader.IsDBNull(7) ? 0 : reader.GetDecimal(7) }
                    });
                    }

                    collection.InsertMany(docs);
                }
            }
        }
    }


    private static void MigrateOrderData()
    {
        var collection = GetMongoCollection("Order");
        using (SqlConnection sql = new SqlConnection(sqlConnectionString))
        {
            sql.Open();
            var cmd = new SqlCommand("SELECT * FROM [Order]", sql);
            using (var reader = cmd.ExecuteReader())
            {
                var docs = new List<BsonDocument>();
                while (reader.Read())
                {
                    var doc = new BsonDocument
                {
                    { "ID", reader.GetInt32(0) },
                    { "CustomerID", reader.GetInt32(1) },
                    { "TotalPrice", reader.GetDecimal(3) }
                };

                    if (!reader.IsDBNull(2))
                        doc.Add("OrderDate", reader.GetDateTime(2));
                    else
                        doc.Add("OrderDate", BsonNull.Value);

                    if (!reader.IsDBNull(4))
                        doc.Add("PickupScheduleId", reader.GetInt32(4));
                    else
                        doc.Add("PickupScheduleId", BsonNull.Value);

                    if (!reader.IsDBNull(5))
                        doc.Add("StatusId", reader.GetInt32(5));
                    else
                        doc.Add("StatusId", BsonNull.Value);

                    docs.Add(doc);
                }
                collection.InsertMany(docs);
            }
        }
    }


    private static void MigrateOrderDetailData()
    {
        var collection = GetMongoCollection("OrderDetail");

        using (var sql = new SqlConnection(sqlConnectionString))
        {
            sql.Open();
            using (var cmd = new SqlCommand("SELECT * FROM OrderDetail", sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var docs = new List<BsonDocument>();
                    while (reader.Read())
                    {
                        docs.Add(new BsonDocument
                    {
                        { "ID", reader.GetInt32(0) },
                        { "OrderID", reader.GetInt32(1) },
                        { "ProductID", reader.GetInt32(2) },
                        { "Quantity", reader.GetInt32(3) },
                        { "SubTotal", reader.GetDecimal(4) },
                        { "Note", reader.IsDBNull(5) ? "" : reader.GetString(5) }
                    });
                    }

                    collection.InsertMany(docs);
                }
            }
        }
    }


    private static void MigrateReceiptData()
    {
        var collection = GetMongoCollection("Receipt");

        using (var sql = new SqlConnection(sqlConnectionString))
        {
            sql.Open();

            using (var cmd = new SqlCommand("SELECT * FROM Receipt", sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var docs = new List<BsonDocument>();

                    while (reader.Read())
                    {
                        var receiptDate = reader.IsDBNull(3)
                            ? BsonNull.Value
                            : BsonValue.Create(reader.GetDateTime(3));

                        var paymentMethodId = reader.IsDBNull(4)
                            ? BsonNull.Value
                            : BsonValue.Create(reader.GetInt32(4));

                        var statusId = reader.IsDBNull(5)
                            ? BsonNull.Value
                            : BsonValue.Create(reader.GetInt32(5));

                        var doc = new BsonDocument
                    {
                        { "ID", BsonValue.Create(reader.GetInt32(0)) },
                        { "OrderID", BsonValue.Create(reader.GetInt32(1)) },
                        { "TotalAmount", BsonValue.Create(reader.GetDecimal(2)) },
                        { "ReceiptDate", receiptDate },
                        { "PaymentMethodId", paymentMethodId },
                        { "StatusId", statusId }
                    };

                        docs.Add(doc);
                    }

                    collection.InsertMany(docs);
                }
            }
        }
    }



    private static void MigratePaymentMethodData()
    {
        var collection = GetMongoCollection("PaymentMethod");

        using (var sql = new SqlConnection(sqlConnectionString))
        {
            sql.Open();
            using (var cmd = new SqlCommand("SELECT * FROM PaymentMethod", sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var docs = new List<BsonDocument>();
                    while (reader.Read())
                    {
                        docs.Add(new BsonDocument
                    {
                        { "ID", reader.GetInt32(0) },
                        { "MethodName", reader.GetString(1) }
                    });
                    }

                    collection.InsertMany(docs);
                }
            }
        }
    }


    private static void MigratePaymentData()
    {
        var collection = GetMongoCollection("Payment");

        using (var sql = new SqlConnection(sqlConnectionString))
        {
            sql.Open();

            using (var cmd = new SqlCommand("SELECT * FROM Payment", sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var docs = new List<BsonDocument>();

                    while (reader.Read())
                    {
                        var paymentDate = reader.IsDBNull(2)
                            ? BsonNull.Value
                            : BsonValue.Create(reader.GetDateTime(2));

                        var statusId = reader.IsDBNull(3)
                            ? BsonNull.Value
                            : BsonValue.Create(reader.GetInt32(3));

                        var paymentMethodId = reader.IsDBNull(4)
                            ? BsonNull.Value
                            : BsonValue.Create(reader.GetInt32(4));

                        var doc = new BsonDocument
                    {
                        { "ID", BsonValue.Create(reader.GetInt32(0)) },
                        { "OrderID", BsonValue.Create(reader.GetInt32(1)) },
                        { "PaymentDate", paymentDate },
                        { "StatusID", statusId },
                        { "PaymentMethodID", paymentMethodId }
                    };

                        docs.Add(doc);
                    }

                    collection.InsertMany(docs);
                }
            }
        }
    }



    private static void MigrateStatusData()
    {
        var collection = GetMongoCollection("Status");

        using (var sql = new SqlConnection(sqlConnectionString))
        {
            sql.Open();
            using (var cmd = new SqlCommand("SELECT * FROM Status", sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var docs = new List<BsonDocument>();
                    while (reader.Read())
                    {
                        docs.Add(new BsonDocument
                    {
                        { "ID", reader.GetInt32(0) },
                        { "StatusName", reader.IsDBNull(1) ? "" : reader.GetString(1) }
                    });
                    }

                    collection.InsertMany(docs);
                }
            }
        }
    }


    private static void MigrateSalesReportData()
    {
        var collection = GetMongoCollection("SalesReport");

        using (var sql = new SqlConnection(sqlConnectionString))
        {
            sql.Open();

            using (var cmd = new SqlCommand("SELECT * FROM SalesReport", sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var docs = new List<BsonDocument>();

                    while (reader.Read())
                    {
                        var reportDate = reader.IsDBNull(1)
                            ? BsonNull.Value
                            : BsonValue.Create(reader.GetDateTime(1));

                        var productId = reader.IsDBNull(2)
                            ? BsonNull.Value
                            : BsonValue.Create(reader.GetInt32(2));

                        var hpp = reader.IsDBNull(3)
                            ? BsonValue.Create(0)
                            : BsonValue.Create(reader.GetDecimal(3));

                        var sellingPrice = reader.IsDBNull(4)
                            ? BsonValue.Create(0)
                            : BsonValue.Create(reader.GetDecimal(4));

                        var margin = reader.IsDBNull(5)
                            ? BsonValue.Create(0)
                            : BsonValue.Create(reader.GetDecimal(5));

                        var openingStock = reader.IsDBNull(6)
                            ? BsonValue.Create(0)
                            : BsonValue.Create(reader.GetInt32(6));

                        var sold = reader.IsDBNull(7)
                            ? BsonValue.Create(0)
                            : BsonValue.Create(reader.GetInt32(7));

                        var vendorHpp = reader.IsDBNull(8)
                            ? BsonValue.Create(0)
                            : BsonValue.Create(reader.GetDecimal(8));

                        var categoryId = reader.IsDBNull(9)
                            ? BsonNull.Value
                            : BsonValue.Create(reader.GetInt32(9));

                        var paymentMethodId = reader.IsDBNull(10)
                            ? BsonNull.Value
                            : BsonValue.Create(reader.GetInt32(10));

                        var reportTime = reader.IsDBNull(11)
                            ? BsonNull.Value
                            : BsonValue.Create(reader.GetDateTime(11));

                        var doc = new BsonDocument
                    {
                        { "ID", BsonValue.Create(reader.GetInt32(0)) },
                        { "ReportDate", reportDate },
                        { "ProductID", productId },
                        { "Hpp", hpp },
                        { "SellingPrice", sellingPrice },
                        { "Margin", margin },
                        { "OpeningStock", openingStock },
                        { "Sold", sold },
                        { "VendorHpp", vendorHpp },
                        { "CategoryID", categoryId },
                        { "PaymentMethodID", paymentMethodId },
                        { "ReportTime", reportTime }
                    };

                        docs.Add(doc);
                    }

                    collection.InsertMany(docs);
                }
            }
        }
    }


    private static void MigratePickupScheduleData()
    {
        var collection = GetMongoCollection("PickupSchedule");

        using (var sql = new SqlConnection(sqlConnectionString))
        {
            sql.Open();

            using (var cmd = new SqlCommand("SELECT * FROM PickupSchedule", sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var docs = new List<BsonDocument>();

                    while (reader.Read())
                    {
                        var pickupEndTime = reader.IsDBNull(3)
                            ? BsonValue.Create("")
                            : BsonValue.Create(reader.GetTimeSpan(3).ToString());

                        var description = reader.IsDBNull(4)
                            ? BsonValue.Create("")
                            : BsonValue.Create(reader.GetString(4));

                        var doc = new BsonDocument
                    {
                        { "ID", BsonValue.Create(reader.GetInt32(0)) },
                        { "PickupDate", BsonValue.Create(reader.GetDateTime(1)) },
                        { "PickupTime", BsonValue.Create(reader.GetTimeSpan(2).ToString()) },
                        { "PickupEndTime", pickupEndTime },
                        { "Description", description }
                    };

                        docs.Add(doc);
                    }

                    collection.InsertMany(docs);
                }
            }
        }
    }


    private static void MigrateUserAuthData()
    {
        var collection = GetMongoCollection("UserAuth");

        using (var sql = new SqlConnection(sqlConnectionString))
        {
            sql.Open();

            using (var cmd = new SqlCommand("SELECT * FROM UserAuth", sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var docs = new List<BsonDocument>();

                    while (reader.Read())
                    {
                        var createdAtValue = reader.IsDBNull(6)
                            ? BsonNull.Value
                            : BsonValue.Create(reader.GetDateTime(6));

                        var doc = new BsonDocument
                    {
                        { "ID", BsonValue.Create(reader.GetInt32(0)) },
                        { "FullName", BsonValue.Create(reader.GetString(1)) },
                        { "Email", BsonValue.Create(reader.GetString(2)) },
                        { "Password", BsonValue.Create(reader.GetString(3)) },
                        { "PhoneNumber", BsonValue.Create(reader.GetString(4)) },
                        { "Role", BsonValue.Create(reader.GetString(5)) },
                        { "CreatedAt", createdAtValue },
                        { "ClassId", BsonValue.Create(reader.GetInt32(7)) }
                    };

                        docs.Add(doc);
                    }

                    collection.InsertMany(docs);
                }
            }
        }
    }


    private static void MigrateCartData()
    {
        var collection = GetMongoCollection("Cart");

        using (var sql = new SqlConnection(sqlConnectionString))
        {
            sql.Open();

            using (var cmd = new SqlCommand("SELECT * FROM Cart", sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var docs = new List<BsonDocument>();

                    while (reader.Read())
                    {
                        var doc = new BsonDocument
                    {
                        { "ID", BsonValue.Create(reader.GetInt32(0)) },
                        { "UserID", BsonValue.Create(reader.GetInt32(1)) },
                        { "ProductID", BsonValue.Create(reader.GetInt32(2)) },
                        { "Quantity", BsonValue.Create(reader.GetInt32(3)) }
                    };

                        docs.Add(doc);
                    }

                    // ✅ Cek apakah ada data sebelum insert
                    if (docs.Count > 0)
                    {
                        collection.InsertMany(docs);
                        Console.WriteLine($"✅ Migrated {docs.Count} Cart data.");
                    }
                    else
                    {
                        Console.WriteLine("⚠️ No Cart data found to migrate.");
                    }
                }
            }
        }
    }


}





using MongoDB.Driver;

public static class MongoDBConnection
{
    private static readonly string connectionUri = "mongodb+srv://mongoBCYuK:mongoDbBcYuk@bcyuk.cjsqfcp.mongodb.net/?retryWrites=true&w=majority&appName=BCYuk";
    private static readonly MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionUri);
    private static readonly MongoClient client;

    static MongoDBConnection()
    {
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        client = new MongoClient(settings);
    }

    public static MongoClient GetClient()
    {
        return client;
    }

    public static IMongoDatabase GetDatabase(string dbName)
    {
        return client.GetDatabase(dbName);
    }
}

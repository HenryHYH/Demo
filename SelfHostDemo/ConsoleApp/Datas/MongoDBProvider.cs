using MongoDB.Driver;

namespace ConsoleApp.Datas
{
    public class MongoDBProvider
    {
        public static IMongoCollection<T> GetCollection<T>(string connectionString, string databaseName)
        {
            IMongoClient client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            return database.GetCollection<T>(typeof(T).Name);
        }
    }
}

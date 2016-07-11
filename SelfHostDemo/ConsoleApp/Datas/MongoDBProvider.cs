using ConsoleApp.Core.Settings;
using MongoDB.Driver;

namespace ConsoleApp.Datas
{
    public class MongoDBProvider
    {
        public static IMongoCollection<T> GetCollection<T>(DataSettings dataSettings)
        {
            IMongoClient client = new MongoClient(dataSettings.RawSettings["MongodbConnectionString"]);
            var database = client.GetDatabase(dataSettings.RawSettings["MongodbDatabase"]);

            return database.GetCollection<T>(typeof(T).Name);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(Helloworld);

            Console.ReadKey();
        }

        public static async void Helloworld()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Helloworld");
            var collection = database.GetCollection<User>("User");

            // await collection.InsertOneAsync(new User { Name = "Jack", Age = 21 });

            //var result = await collection.DeleteManyAsync(x => x.Age == 20);
            //Console.WriteLine("Delete count = " + result.DeletedCount);

            //var update = Builders<User>.Update.Set(x => x.Name, "Jack2");
            var update = Builders<User>.Update.Inc(x => x.Age, 1);
            var updateResult = await collection.UpdateManyAsync(x => x.Name == "Jack", update);
            Console.WriteLine("Count = " + updateResult.ModifiedCount);

            var list = await collection.Find(Builders<User>.Filter.Empty)
                                        .ToListAsync();

            foreach (var item in list)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Age);
            }
        }
    }
}

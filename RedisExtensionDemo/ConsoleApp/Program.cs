using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ISerializer serializer = new NewtonsoftSerializer();
            var c = ConnectionMultiplexer.Connect("localhost");
            ICacheClient client = new StackExchangeRedisCacheClient(c, serializer);
            client.Add("Test", new User()
            {
                Name = "Test",
                Age = 100,
                Birthday = DateTime.Now
            });

            Console.WriteLine(client.Get<User>("Test").ToString());

            Console.ReadKey();
        }
    }

    public class User
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime Birthday { get; set; }

        public override string ToString()
        {
            return string.Format("{0}_{1}_{2:yyyy-MM-dd HH:mm:ss}", Name, Age, Birthday);
        }
    }
}

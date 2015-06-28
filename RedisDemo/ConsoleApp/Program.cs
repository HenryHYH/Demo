using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = "127.0.0.1";
            int port = 6379;

            using (var client = new RedisClient(host, port))
            {
                var hello = client.Get<string>("hello");
                if (string.IsNullOrWhiteSpace(hello))
                {
                    client.Set<string>("hello", "henry");
                    Console.WriteLine("Write");
                }
                else
                    Console.WriteLine(hello);
            }

            Console.ReadLine();
        }
    }
}

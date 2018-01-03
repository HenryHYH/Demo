using Newtonsoft.Json;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start - {0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now);

            var data = RestSharpClient.Send<TrademarkDetail>("8556153", "25");
            Console.WriteLine("Data = " + JsonConvert.SerializeObject(data, Formatting.Indented));

            Console.WriteLine("Finish - {0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now);
            Console.ReadKey();
        }
    }
}

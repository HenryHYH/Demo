using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApiSdk;
using WebApiSdk.Common;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start.");
            Console.ReadKey();

            Console.WriteLine("Start");
            RunAsync().Wait();
            Console.WriteLine("Finish");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static async Task RunAsync()
        {
            try
            {
                var req = new ValuesRequest();
                var res = await req.Execute();
                Show(res.Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Show(IEnumerable<string> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}

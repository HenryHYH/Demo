using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test();
        }

        private static void Test()
        {
            var sw = new Stopwatch();
            IList<double> times = new List<double>();

            Parallel.For(0, 1000, new ParallelOptions()
            {
                MaxDegreeOfParallelism = 50
            }, i =>
            {
                sw.Restart();

                Console.WriteLine("i = {0}", i);

                var hander = new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip };
                using (var client = new HttpClient(hander))
                {
                    var response = client.GetAsync("http://localhost:8000/api/user");
                    var str = response.Result.Content.ReadAsStringAsync();
                    Console.WriteLine(str.Result);
                }

                sw.Stop();
                times.Add(sw.Elapsed.TotalMilliseconds);
            });

            Console.WriteLine("Avg = " + times.Average());
            Console.ReadKey();
        }
    }
}

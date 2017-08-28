using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static HttpClient client = new HttpClient();

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
            client.BaseAddress = new Uri("http://localhost:4598/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var list = await GetAsync<IList<string>>("api/values");
                Show(list);
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

        static async Task<T> GetAsync<T>(string path)
        {
            var result = default(T);

            var response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<T>();
            }

            return result;
        }
    }
}

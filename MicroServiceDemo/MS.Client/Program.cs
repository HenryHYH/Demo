using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MS.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
            Console.ReadKey();
        }

        static async Task Run()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8802/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("product");
                if (response.IsSuccessStatusCode)
                {
                    IList<Product> products = await response.Content.ReadAsAsync<IList<Product>>();
                    foreach (var product in products)
                        Console.WriteLine("Name={0};\r\nPrice={1};\r\nCategory={2};\r\n", product.Name, product.Price, product.Category);
                }
            }
        }
    }

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }
    }
}

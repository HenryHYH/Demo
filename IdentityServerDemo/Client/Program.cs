using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            await Test();

            Console.ReadKey();
        }

        public static async Task Test()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ClientID", "abc");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("API");
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            var response = await client.GetAsync("http://localhost:5002/api/values");
            if (!response.IsSuccessStatusCode)
                Console.WriteLine(response.StatusCode);
            else
                Console.WriteLine($"请求结果：{response.StatusCode}");
        }
    }
}

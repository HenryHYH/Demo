using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Start");
            MainImpl().GetAwaiter().GetResult();
            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        private static async Task MainImpl()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            var tokenRes = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });
            if (tokenRes.IsError)
            {
                Console.WriteLine(tokenRes.Error);
                return;
            }
            Console.WriteLine(tokenRes.Json);

            client = new HttpClient();
            client.SetBearerToken(tokenRes.AccessToken);

            var res = await client.GetAsync("http://localhost:5001/identity");
            if (!res.IsSuccessStatusCode)
                Console.WriteLine(res.StatusCode);
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}

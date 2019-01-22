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

            TokenResponse tokenRes = null;
            Console.WriteLine("输入模式，1为客户端模式，2为用户名密码模式");
            if ("1" == Console.ReadLine())
            {
                tokenRes = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "client",
                    ClientSecret = "secret",
                    Scope = "api1"
                });
            }
            else
            {
                tokenRes = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "ro.client",
                    ClientSecret = "secret",
                    Scope = "api1",
                    UserName = "henry",
                    Password = "mytest"
                });
            }
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

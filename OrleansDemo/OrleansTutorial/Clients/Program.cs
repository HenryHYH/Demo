using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using System;
using System.Threading.Tasks;

namespace OrleansTutorial
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            return MainAsync().Result;
        }

        private static async Task<int> MainAsync()
        {
            try
            {
                using (var client = await ConnectClient())
                {
                    await DoClientWork(client);
                    Console.ReadLine();
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nException {ex}");
                Console.WriteLine("\nPress any key to exit.");
                Console.ReadLine();

                return 1;
            }
        }

        private static async Task<IClusterClient> ConnectClient()
        {
            var client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(o =>
                {
                    o.ClusterId = "dev";
                    o.ServiceId = "OrleansTutorial";
                })
                .ConfigureLogging(l => l.AddConsole())
                .Build();

            await client.Connect();
            Console.WriteLine("Client successfully connected");

            return client;
        }

        private static async Task DoClientWork(IClusterClient client)
        {
            var friend = client.GetGrain<IHello>(0);
            var res = await friend.SayHello("Hello world");
            Console.WriteLine($"\n\n{res}\n\n");
        }
    }
}

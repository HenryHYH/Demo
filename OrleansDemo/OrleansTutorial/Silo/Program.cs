using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
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
                var host = await StartSilo();
                Console.WriteLine("\n\n Press Enter to exit...\n\n");
                Console.ReadLine();

                await host.StopAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(o =>
                {
                    o.ClusterId = "dev";
                    o.ServiceId = "OrleansTutorial";
                })
                .ConfigureApplicationParts(c => c.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
                .ConfigureLogging(c => c.AddConsole());

            var host = builder.Build();
            await host.StartAsync();

            return host;
        }
    }
}

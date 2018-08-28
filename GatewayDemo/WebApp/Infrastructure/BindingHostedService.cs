using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Infrastructure
{
    public class BindingHostedService : Microsoft.Extensions.Hosting.IHostedService
    {
        private readonly IApplicationLifetime lifetime;

        public BindingHostedService(IApplicationLifetime lifetime)
        {
            this.lifetime = lifetime;
        }

        public static IApplicationBuilder Application { get; set; }

        public static IServerAddressesFeature ServerAddresses { get; set; }

        public static Uri Uri { get; set; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var address = ServerAddresses.Addresses.Single();
            Uri = new Uri(address);

            if (Uri.Host == "localhost" || Uri.Host == "[::]")
            {
                var hostName = System.Net.Dns.GetHostName();
                var host = System.Net.Dns.GetHostEntry(hostName);
                var ip = host.AddressList.First(x => !x.IsIPv6LinkLocal).ToString();
                Uri = new Uri($"{Uri.Scheme}://{ip}:{Uri.Port}");
            }

            Application.RegisterWithConsul(Uri, lifetime);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

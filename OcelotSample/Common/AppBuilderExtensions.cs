using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Common
{
    public static class AppBuilderExtensions
    {
        private static void AddHealthCheck(IApplicationBuilder app)
        {
            app.Map("/hc", x => x.Run(async context => await context.Response.WriteAsync(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"))));
        }

        public static IApplicationBuilder RegisterConsul(
            this IApplicationBuilder app,
            IApplicationLifetime lifetime,
            IConfiguration configuration)
        {
            var serverAddressesFeature = app.ServerFeatures.Get<IServerAddressesFeature>();
            var serverAddress = serverAddressesFeature.Addresses
                                    .Where(x => x.Contains("*"))
                                    .FirstOrDefault();
            if (string.IsNullOrWhiteSpace(serverAddress))
                return app;

            AddHealthCheck(app);

            var serviceIp = configuration["Consul:Service:IP"];
            var serviceName = configuration["Consul:Service:Name"];
            var client = new ConsulClient(x => x.Address = new Uri(configuration["Consul:Address"]));
            serverAddress = serverAddress.Replace("*", serviceIp);
            var serverUri = new Uri(serverAddress);

            var httpCheck = new AgentServiceCheck
            {
                // 服务启动后多久注册
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                // 心跳间隔
                Interval = TimeSpan.FromSeconds(10),
                // 健康检查地址
                HTTP = $"{serverAddress}/hc",
                Timeout = TimeSpan.FromSeconds(5)
            };

            var registration = new AgentServiceRegistration
            {
                ID = $"{serviceName}-{Guid.NewGuid().ToString()}-{serverUri.Port}",
                Checks = new[] { httpCheck },
                Name = serviceName,
                Address = serviceIp,
                Port = serverUri.Port,
                Tags = new[] { $"urlprefix-/{serviceName}" }
            };

            client.Agent.ServiceDeregister(registration.ID).Wait();
            client.Agent.ServiceRegister(registration).Wait();
            lifetime.ApplicationStopping.Register(() =>
            {
                client.Agent.ServiceDeregister(registration.ID).Wait();
            });

            return app;
        }
    }
}

using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace WebApp.Infrastructure
{
    public static class ConsulExtensions
    {
        public static IApplicationBuilder RegisterWithConsul(this IApplicationBuilder app, Uri uri, IApplicationLifetime lifetime)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var consulConfig = app.ApplicationServices.GetRequiredService<IOptions<ConsulConfig>>();
            var loggingFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var logger = loggingFactory.CreateLogger<IApplicationBuilder>();

            var registration = new AgentServiceRegistration
            {
                ID = $"{consulConfig.Value.ServiceId}-{uri.Port}",
                Name = consulConfig.Value.ServiceName,
                Address = $"{(uri.Host == "[::]" ? "localhost" : uri.Host)}",
                Port = uri.Port,
                Tags = new[] { "WebApp" }
            };

            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            consulClient.Agent.ServiceRegister(registration).Wait();

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Deregistering from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });

            return app;
        }
    }
}

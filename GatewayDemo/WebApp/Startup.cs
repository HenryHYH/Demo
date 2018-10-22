using Consul;
using Exceptionless;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using WebApp.Infrastructure;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Consul
            services.Configure<ConsulConfig>(Configuration.GetSection("consulConfig"));
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(c =>
            {
                var address = Configuration["consulConfig:address"];
                c.Address = new System.Uri(address);
            }));

            // Logging
            services.AddLogging(c => c.AddConsole());

            // MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // IP
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BindingHostedService>();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Swagger XML Api Demo",
                    Version = "v1"
                });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            // Exceptionless
            ExceptionlessClient.Default.Configuration.ApiKey = "jharUELJtv1I5XTLsjBQF1UCV6GNtkQ729033z2K";
            ExceptionlessClient.Default.Configuration.ServerUrl = "http://192.168.5.209:10080";
            //ExceptionlessClient.Default.Configuration.UseFileLogger("d:\\exceptionless.log", Exceptionless.Logging.LogLevel.Trace);
            app.UseExceptionless();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger XML Api Demo v1");
                c.RoutePrefix = string.Empty;
            });

            BindingHostedService.Application = app;
            BindingHostedService.ServerAddresses = app.ServerFeatures.Get<IServerAddressesFeature>();
        }
    }
}

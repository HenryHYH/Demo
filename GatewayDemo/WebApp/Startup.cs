using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            services.Configure<ConsulConfig>(Configuration.GetSection("consulConfig"));
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(c =>
            {
                var address = Configuration["consulConfig:address"];
                c.Address = new System.Uri(address);
            }));

            services.AddLogging(c => c.AddConsole());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BindingHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            BindingHostedService.Application = app;
            BindingHostedService.ServerAddresses = app.ServerFeatures.Get<IServerAddressesFeature>();
        }
    }
}

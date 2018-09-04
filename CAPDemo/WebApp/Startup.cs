using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            // SqlServer
            services.AddDbContext<AppDbContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // CAP
            services.AddCap(x =>
            {
                // Dashboard
                x.UseDashboard();

                // EF
                x.UseEntityFramework<AppDbContext>();

                // Dapper
                //x.UseSqlServer("ConnectionString");

                // RabbitMq
                x.UseRabbitMQ(c =>
                {
                    c.HostName = "henry.com";
                    c.UserName = "admin";
                    c.Password = "admin";
                });

                /*
                x.UseDiscovery(c =>
                {
                    c.DiscoveryServerHostName = "henry.com";
                    c.DiscoveryServerPort = 8500;
                    c.CurrentNodeHostName = "192.168.7.32";
                    c.CurrentNodePort = 5000;
                    c.NodeId = 1;
                    c.NodeName = "node4";
                });
                */
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

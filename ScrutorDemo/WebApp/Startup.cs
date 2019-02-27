using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Services;

namespace WebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddTransient<IUserService, UserService>();
            services.Decorate<IUserService, WrappedUserService>();

            services.Scan(s =>
            {
                //s.FromAssemblyOf<Startup>()
                s.FromCallingAssembly()
                    .AddClasses()
                    .AsSelf()
                    .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}

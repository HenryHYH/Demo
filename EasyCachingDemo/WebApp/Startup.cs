﻿using EasyCaching.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddDefaultInMemoryCache(Configuration);
            //services.AddDefaultInMemoryCache(x =>
            //{
            //    x.MaxRdSecond = 0;
            //});

            //services.AddDefaultRedisCache(o =>
            //{
            //    o.MaxRdSecond = 0;
            //    o.DBConfig.Endpoints.Add(new EasyCaching.Core.Configurations.ServerEndPoint("henry.cn", 6379));
            //});

            services.AddSQLiteCache(o =>
            {
                o.MaxRdSecond = 0;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSQLiteCache();
        }
    }
}

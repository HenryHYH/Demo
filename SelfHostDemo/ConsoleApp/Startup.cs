using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using ConsoleApp.Core.Caching;
using ConsoleApp.Core.MessageQueues;
using ConsoleApp.Core.Settings;
using ConsoleApp.Infrastructure.WebApi;
using ConsoleApp.Mappings;
using ConsoleApp.Services.Logging;
using ConsoleApp.Services.Users;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace ConsoleApp
{
    public class Startup
    {
        private HttpConfiguration config;
        private IAppBuilder app;

        public void Configuration(IAppBuilder app)
        {
#if DEBUG
            app.UseErrorPage();
#endif

            this.config = new HttpConfiguration();
            this.app = app;

            // Autofac
            UseAutofac();

            // Web Api
            UseWebApi();

            // File Server
            UseFileServer();

            // AutoMapper
            ModelMapper.MapModels();
        }

        private void UseAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterWebApiFilterProvider(config);

            // data
            var dataSettingsManager = new DataSettingsManager();
            builder.Register(x => dataSettingsManager.LoadSettings()).As<DataSettings>();

            // caching
            builder.RegisterType<RedisCacheManager>().As<ICacheManager>().SingleInstance();

            // mq
            builder.RegisterType<MSMessageQueueManager>().As<IMessageQueueManager>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<FileLogger>().As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
        }

        private void UseWebApi()
        {
            // default action api
            config.Routes.MapHttpRoute(
                name: "DefaultActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "Get", id = RouteParameter.Optional }
            );
            // default api
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { action = "Get", id = RouteParameter.Optional }
            );

            config.Formatters.Add(new BrowserJsonFormatter());
            app.UseWebApi(config);
        }

        private void UseFileServer()
        {
            var options = new FileServerOptions
            {
                EnableDirectoryBrowsing = true,
                EnableDefaultFiles = true,
                DefaultFilesOptions = { DefaultFileNames = { "index.html" } },
                FileSystem = new PhysicalFileSystem("Webroot"),
                StaticFileOptions = { ContentTypeProvider = new CustomContentTypeProvider() }
            };

            app.UseFileServer(options);
        }
    }
}

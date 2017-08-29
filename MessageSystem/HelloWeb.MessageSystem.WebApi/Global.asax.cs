using Autofac;
using Autofac.Integration.Mvc;
using HelloWeb.MessageSystem.Core.Data;
using HelloWeb.MessageSystem.Core.Service;
using HelloWeb.MessageSystem.Core.Setting;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace HelloWeb.MessageSystem.WebApi
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.Register(x => SettingFactory.Create<SystemSetting>()).As<SystemSetting>().SingleInstance();
            builder.RegisterGeneric(typeof(MongoRepository<>)).As(typeof(IBaseRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<LogService>().As<ILogService>().InstancePerLifetimeScope();

            builder.RegisterControllers(typeof(Global).Assembly);
            builder.RegisterModelBinders(typeof(Global).Assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using HelloWeb.MessageSystem.Core.Infrastructure;
using System.Web.Http;

namespace HelloWeb.MessageSystem.WebApi
{
    /// <summary>
    /// 依赖配置
    /// </summary>
    public class DependencyConfig
    {
        /// <summary>
        /// 注册依赖信息
        /// </summary>
        /// <returns></returns>
        public static IContainer Register(HttpConfiguration config)
        {
            return DependencyRegistrar.Register(builder =>
            {
                builder.RegisterApiControllers(typeof(Global).Assembly);
                builder.RegisterWebApiFilterProvider(config);
                builder.RegisterWebApiModelBinderProvider();

                builder.RegisterControllers(typeof(Global).Assembly);
                builder.RegisterModelBinders(typeof(Global).Assembly);
                builder.RegisterModelBinderProvider();
                builder.RegisterModule<AutofacWebTypesModule>();
                builder.RegisterSource(new ViewRegistrationSource());
                builder.RegisterFilterProvider();
            });
        }
    }
}
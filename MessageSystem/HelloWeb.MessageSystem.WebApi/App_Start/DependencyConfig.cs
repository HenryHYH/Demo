using Autofac;
using Autofac.Integration.Mvc;
using HelloWeb.MessageSystem.Core.Infrastructure;

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
        public static IContainer Register()
        {
            return DependencyRegistrar.Register(builder =>
            {
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
namespace FW.Web.Framework
{
    using System.Linq;

    using Autofac;
    using Autofac.Integration.Mvc;

    using FW.Core.Caching;
    using FW.Core.Data;
    using FW.Core.Infrastructure;
    using FW.Data;
    using FW.Service.Localization;
    using FW.Service.Logging;
    using FW.Service.Users;
    using FW.Web.Framework.UI;

    public class DependencyRegister : IDependencyRegister
    {
        #region Properties

        public int Order
        {
            get { return 0; }
        }

        #endregion Properties

        #region Methods

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //Framework.UI
            builder.RegisterType<PageBulider>().As<IPageBulider>().InstancePerLifetimeScope();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            // Core
            builder.Register(x => new SettingsManager().LoadSettings()).As<Settings>();
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().SingleInstance();

            // Repository
            builder.RegisterGeneric(typeof(MongoRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            // Services
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<DBLogger>().As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterType<LocalizationService>().As<ILocalizationService>().InstancePerLifetimeScope();
        }

        #endregion Methods
    }
}
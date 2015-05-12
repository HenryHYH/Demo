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

            //data layer
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            builder.Register(c => dataSettingsManager.LoadSettings()).As<DataSettings>();
            builder.Register(x => new EfDataProviderManager(x.Resolve<DataSettings>())).As<BaseDataProviderManager>().InstancePerDependency();
            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();
            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                var efDataProviderManager = new EfDataProviderManager(dataSettingsManager.LoadSettings());
                var dataProvider = efDataProviderManager.LoadDataProvider();
                dataProvider.InitConnectionFactory();

                builder.Register<IDbContext>(c => new NopObjectContext(dataProviderSettings.DataConnectionString)).InstancePerRequest();
            }
            else
            {
                builder.Register<IDbContext>(c => new NopObjectContext(dataSettingsManager.LoadSettings().DataConnectionString)).InstancePerRequest();
            }

            // Repository
            //builder.RegisterGeneric(typeof(MongoRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            // Services
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<DBLogger>().As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterType<LocalizationService>().As<ILocalizationService>().InstancePerLifetimeScope();
        }

        #endregion Methods
    }
}
namespace FW.Web.Framework
{
    using System.Linq;

    using Autofac;
    using Autofac.Integration.Mvc;

    using FW.Core.Data;
    using FW.Core.Infrastructure;
    using FW.Data;
    using FW.Service.Users;
    using FW.Web.Framework.UI;
    using FW.Service.Logging;

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

            // Settings
            builder.Register(x => new SettingsManager().LoadSettings()).As<Settings>();

            // Repository
            builder.RegisterGeneric(typeof(MongoRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            // Services
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<DBLogger>().As<ILogger>().InstancePerLifetimeScope();
        }

        #endregion Methods
    }
}
using Autofac;
using Autofac.Integration.Mvc;
using FW.Core.Data;
using FW.Core.Infrastructure;
using FW.Data;
using FW.Service.Users;
using System.Linq;

namespace FW.Web.Framework
{
    public class DependencyRegister : IDependencyRegister
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            // Repository
            builder.RegisterGeneric(typeof(MongoRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            // Services
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}

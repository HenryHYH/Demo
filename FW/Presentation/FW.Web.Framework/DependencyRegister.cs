using Autofac;
using FW.Core;
using FW.Core.Data;
using FW.Domain.Infrastructure;

namespace FW.Web.Framework
{
    public class DependencyRegister : IDependencyRegister
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType(typeof(MongoRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}

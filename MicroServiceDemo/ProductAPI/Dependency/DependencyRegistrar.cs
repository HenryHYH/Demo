using Autofac;
using MS.Infrastructure;

namespace MS.ProductAPI.Dependency
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        #region Methods

        public virtual void Register(ContainerBuilder bulider, ITypeFinder typeFinder)
        {
            bulider.RegisterType<StartupService>().SingleInstance();
        }

        #endregion

        #region Properties

        public int Order
        {
            get { return 1; }
        }

        #endregion
    }
}

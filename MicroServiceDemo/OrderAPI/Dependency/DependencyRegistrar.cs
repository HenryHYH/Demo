using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Topshelf.Autofac;
using Autofac.Integration.WebApi;
using MS.Infrastructure;
using System.Reflection;

namespace MS.OrderAPI.Dependency
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

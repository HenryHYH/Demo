using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace MS.Infrastructure
{
    public class Engine : IEngine
    {
        #region Fields

        private ContainerManager containerManager;

        #endregion

        #region Properties

        public ContainerManager ContainerManager
        {
            get { return containerManager; }
        }

        #endregion

        #region Utilities

        protected virtual void RegisterDependencies(ServerConfig config)
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();
            containerManager = new ContainerManager(container);

            builder = new ContainerBuilder();
            var typeFinder = new AppDomainTypeFinder();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            builder.RegisterInstance(config).As<ServerConfig>().SingleInstance();
            builder.Update(container);

            builder = new ContainerBuilder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
            drInstances = drInstances.OrderBy(x => x.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder);
            builder.Update(container);
        }

        #endregion

        #region Methods

        public void Initialize(ServerConfig config)
        {
            RegisterDependencies(config);
        }

        public T Resolve<T>() where T : class
        {
            return containerManager.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return containerManager.Resolve(type);
        }

        public T[] ResolveAll<T>()
        {
            return containerManager.ResolveAll<T>();
        }

        #endregion
    }
}

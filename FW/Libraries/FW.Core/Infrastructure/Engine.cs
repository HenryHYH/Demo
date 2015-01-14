namespace FW.Core.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;

    public class Engine : IEngine
    {
        #region Fields

        private ContainerManager containerManager;

        #endregion Fields

        #region Methods

        public void Initialize()
        {
            RegisterDependencies();
            RunStartupTasks();
        }

        protected virtual void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            var typeFinder = new WebAppTypeFinder();
            builder = new ContainerBuilder();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            builder.Update(container);

            builder = new ContainerBuilder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegister>();
            var drInstances = new List<IDependencyRegister>();
            foreach (var type in drTypes)
                drInstances.Add((IDependencyRegister)Activator.CreateInstance(type));
            drInstances = drInstances.AsQueryable().OrderBy(x => x.Order).ToList();
            foreach (var dependencyRegister in drInstances)
                dependencyRegister.Register(builder, typeFinder);
            builder.Update(container);

            containerManager = new ContainerManager(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        protected virtual void RunStartupTasks()
        {
            var typeFinder = containerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            //sort
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }

        public T Resolve<T>()
            where T : class
        {
            return containerManager.Resolve<T>();
        }

        public ContainerManager ContainerManager
        {
            get
            {
                return containerManager;
            }
        }

        #endregion Methods
    }
}
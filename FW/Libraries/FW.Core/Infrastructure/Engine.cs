using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FW.Core.Infrastructure
{
    public class Engine : IEngine
    {
        public void Initialize()
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

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}

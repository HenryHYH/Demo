using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core.Lifetime;

namespace MS.Infrastructure
{
    public class ContainerManager
    {
        #region Fields

        private readonly IContainer container;

        #endregion

        #region Ctor

        public ContainerManager(IContainer container)
        {
            this.container = container;
        }

        #endregion

        #region Properties

        public virtual IContainer Container
        {
            get
            {
                return container;
            }
        }

        #endregion

        #region Methods

        public virtual T Resolve<T>(string key = "", ILifetimeScope scope = null)
                        where T : class
        {
            if (null == scope)
                scope = Scope();

            if (string.IsNullOrEmpty(key))
                return scope.Resolve<T>();

            return scope.ResolveKeyed<T>(key);
        }

        public virtual object Resolve(Type type, ILifetimeScope scope = null)
        {
            if (null == scope)
                scope = Scope();

            return scope.Resolve(type);
        }

        public virtual T[] ResolveAll<T>(string key = "", ILifetimeScope scope = null)
        {
            if (null == scope)
                scope = Scope();

            if (string.IsNullOrEmpty(key))
                return scope.Resolve<IEnumerable<T>>().ToArray();

            return scope.ResolveKeyed<IEnumerable<T>>(key).ToArray();
        }

        public virtual T ResolveUnregistered<T>(ILifetimeScope scope = null)
            where T : class
        {
            return ResolveUnregistered(typeof(T), scope) as T;
        }

        public virtual object ResolveUnregistered(Type type, ILifetimeScope scope = null)
        {
            if (null == scope)
                scope = Scope();

            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = constructor.GetParameters();
                    var parameterInstances = new List<object>();
                    foreach (var parameter in parameters)
                    {
                        var service = Resolve(parameter.ParameterType, scope);
                        if (null != service)
                            parameterInstances.Add(service);
                    }

                    return Activator.CreateInstance(type, parameterInstances.ToArray());
                }
                catch
                {
                }
            }

            throw new Exception("No contructor was found");
        }

        public virtual bool TryResolve(Type serviceType, ILifetimeScope scope, out object instance)
        {
            if (null == scope)
                scope = Scope();

            return scope.TryResolve(serviceType, out instance);
        }

        public virtual bool IsRegistered(Type serviceType, ILifetimeScope scope = null)
        {
            if (null == scope)
                scope = Scope();

            return scope.IsRegistered(serviceType);
        }

        public virtual object ResolveOptional(Type serviceType, ILifetimeScope scope = null)
        {
            if (null == scope)
                scope = Scope();

            return scope.ResolveOptional(serviceType);
        }

        public virtual ILifetimeScope Scope()
        {
            return container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
        }

        #endregion
    }
}

﻿namespace FW.Core.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    using Autofac;
    using Autofac.Core.Lifetime;
    using Autofac.Integration.Mvc;

    public class ContainerManager
    {
        #region Fields

        private readonly IContainer _container;

        #endregion Fields

        #region Constructors

        public ContainerManager(IContainer container)
        {
            _container = container;
        }

        #endregion Constructors

        #region Properties

        public IContainer Container
        {
            get { return _container; }
        }

        #endregion Properties

        #region Methods

        public T Resolve<T>(string key = "", ILifetimeScope scope = null)
            where T : class
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            if (string.IsNullOrEmpty(key))
            {
                return scope.Resolve<T>();
            }
            return scope.ResolveKeyed<T>(key);
        }

        public ILifetimeScope Scope()
        {
            try
            {
                if (HttpContext.Current != null)
                    return AutofacDependencyResolver.Current.RequestLifetimeScope;

                //when such lifetime scope is returned, you should be sure that it'll be disposed once used (e.g. in schedule tasks)
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
            catch (Exception)
            {
                //we can get an exception here if RequestLifetimeScope is already disposed
                //for example, requested in or after "Application_EndRequest" handler
                //but note that usually it should never happen

                //when such lifetime scope is returned, you should be sure that it'll be disposed once used (e.g. in schedule tasks)
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
        }

        #endregion Methods
    }
}
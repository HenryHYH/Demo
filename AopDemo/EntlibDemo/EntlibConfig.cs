using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace EntlibDemo
{
    public static class EntlibConfig
    {
        public static void Configuration()
        {
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            PolicyInjector policyInjector = new PolicyInjector(configurationSource);
            PolicyInjection.SetPolicyInjector(policyInjector);
        }

        public static IUnityContainer Container { get; private set; }

        static EntlibConfig()
        {
            Container = new UnityContainer();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static void Initialize()
        {
            Container.AddNewExtension<Interception>();
            Container.RegisterType<IUserService, UserService>(
                new InterceptionBehavior<PolicyInjectionBehavior>(),
                new Interceptor<TransparentProxyInterceptor>()
            );
        }
    }
}

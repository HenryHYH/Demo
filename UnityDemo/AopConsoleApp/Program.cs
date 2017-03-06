using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();

            //RegisterByCode(ref container);
            RegisterByConfig(ref container);

            var logger = container.Resolve<ILog>();
            logger.Log("Hello world");

            Console.ReadKey();
        }

        private static void RegisterByConfig(ref IUnityContainer container)
        {
            container.LoadConfiguration();
        }

        private static void RegisterByCode(ref IUnityContainer container)
        {
            container.AddNewExtension<Interception>();
            container.RegisterType<ILog, ConsoleLog>(
                    new Interceptor<InterfaceInterceptor>(),
                    new InterceptionBehavior<AOP.LoggingInterceptionBehavior>(),
                    new InterceptionBehavior<AOP.PerformanceInterceptionBehavior>()
                );
        }
    }
}

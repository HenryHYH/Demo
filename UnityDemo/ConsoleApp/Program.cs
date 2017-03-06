using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 注册
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
            container.RegisterType<ILog, ConsoleLog>();
        }
    }
}

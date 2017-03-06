using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Services;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            //RegisterByCode(ref container);
            RegisterByConfig(ref container);

            var userService = container.Resolve<IUserService>();
            Console.WriteLine(userService.GetName());

            Console.ReadKey();
        }

        private static void RegisterByConfig(ref IUnityContainer container)
        {
            container.LoadConfiguration();
        }

        private static void RegisterByCode(ref IUnityContainer container)
        {
            container.RegisterType<IRepository, SqlRepository>();
            container.RegisterType<IRepository, OracleRepository>("Oracle");
            container.RegisterType<IUserService, UserService>();
        }
    }
}

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Services;
using Services.Repository;
using System;

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

            var sqlRepository = container.Resolve<IRepository>();
            Console.WriteLine(sqlRepository.Get());
            var oracleRepository = container.Resolve<IRepository>("Oracle");
            Console.WriteLine(oracleRepository.Get());

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

using IoCDemo.Services;
using Ninject;
using System;

namespace IoCDemo.NinjectDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //ConfigByCode();
            //ConfigByModule();
            //ConfigByModuleAssemblies();
            //ConfigByXml();
            ConfigByServiceContext();

            Console.ReadKey();
        }

        private static void ConfigByServiceContext()
        {
            var service = ServiceContext.Current.Resolve<IService>();
            service.Save();
        }

        private static void ConfigByCode()
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Bind<IDal>().To<MsSqlDal>();
                kernel.Bind<IService>().To<TestService>();

                var service = kernel.Get<IService>();
                service.Save();
            }
        }

        private static void ConfigByModule()
        {
            using (IKernel kernel = new StandardKernel(new TestServiceConfigModule()))
            {
                var service = kernel.Get<IService>();
                service.Save();
            }
        }

        private static void ConfigByModuleAssemblies()
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Load(AppDomain.CurrentDomain.GetAssemblies());
                var service = kernel.Get<IService>();
                service.Save();
            }
        }

        private static void ConfigByXml()
        {
            using (IKernel kernel = new StandardKernel())
            {
                //kernel.Load("TestServiceConfig.xml");
                kernel.Load("*.xml");

                var service = kernel.Get<IService>();
                service.Save();
            }
        }
    }
}

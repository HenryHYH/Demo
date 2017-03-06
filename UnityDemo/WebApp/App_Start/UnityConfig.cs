using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Services;
using Services.Repository;
using Microsoft.Practices.Unity.Configuration;

namespace WebApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            // 使用代码进行配置
            //container.RegisterType<IRepository, SqlRepository>();
            //container.RegisterType<IUserService, UserService>();

            // 读取配置文件的配置
            container.LoadConfiguration();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
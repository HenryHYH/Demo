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
            // ʹ�ô����������
            //container.RegisterType<IRepository, SqlRepository>();
            //container.RegisterType<IUserService, UserService>();

            // ��ȡ�����ļ�������
            container.LoadConfiguration();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
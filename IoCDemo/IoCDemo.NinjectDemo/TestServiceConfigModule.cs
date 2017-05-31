using IoCDemo.Services;
using Ninject.Modules;

namespace IoCDemo.NinjectDemo
{
    public class TestServiceConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDal>().To<MsSqlDal>();
            Bind<IService>().To<TestService>();
        }
    }
}

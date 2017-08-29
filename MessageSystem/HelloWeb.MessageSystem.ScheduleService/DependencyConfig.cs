using Autofac;
using HelloWeb.MessageSystem.Core.Data;
using HelloWeb.MessageSystem.Core.Service;
using HelloWeb.MessageSystem.Core.Setting;

namespace HelloWeb.MessageSystem.ScheduleService
{
    public static class DependencyConfig
    {
        public static IContainer Container { get; private set; }

        public static IContainer Register()
        {
            var builder = new ContainerBuilder();

            builder.Register(x => SettingHelper.Load<SystemSetting>()).As<SystemSetting>().SingleInstance();
            builder.RegisterGeneric(typeof(MongoRepository<>)).As(typeof(IBaseRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<LogService>().As<ILogService>().InstancePerLifetimeScope();

            builder.RegisterType<ProgramService>();
            builder.RegisterType<TestJob>();

            Container = builder.Build();

            return Container;
        }
    }
}

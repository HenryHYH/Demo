using Autofac;
using HelloWeb.MessageSystem.Core.Infrastructure;

namespace HelloWeb.MessageSystem.ScheduleService
{
    public static class DependencyConfig
    {
        public static IContainer Register()
        {
            return DependencyRegistrar.Register(builder =>
            {
                builder.RegisterType<ProgramService>();
                builder.RegisterType<TestJob>();
            });
        }
    }
}

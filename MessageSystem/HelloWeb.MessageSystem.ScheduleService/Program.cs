using Topshelf;
using Topshelf.Autofac;

namespace HelloWeb.MessageSystem.ScheduleService
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = DependencyConfig.Register();

            HostFactory.Run(x =>
            {
                x.UseAutofacContainer(container);

                x.Service<ProgramService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted(f => f.Start());
                    s.WhenStopped(f => f.Stop());
                    s.WhenPaused(f => f.Pause());
                    s.WhenContinued(f => f.Continue());
                });

                x.RunAsLocalService();
                x.SetDisplayName("HelloWeb ScheduleService");
                x.SetDescription("Schedule service for Logging");
                x.SetServiceName("HelloWeb.MessageSystem.ScheduleService");
                x.EnablePauseAndContinue();
            });
        }
    }
}

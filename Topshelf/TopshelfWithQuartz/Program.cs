using Topshelf;

namespace TopshelfWithQuartz
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<QuartzService>(s =>
                {
                    s.ConstructUsing(n => new QuartzService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                    s.WhenPaused(tc => tc.Pause());
                    s.WhenContinued(tc => tc.Continue());
                });

                x.RunAsLocalService();
                x.SetServiceName("Name");
                x.SetDescription("Description");
                x.SetDisplayName("DisplayName");
                x.EnablePauseAndContinue();
            });
        }
    }
}

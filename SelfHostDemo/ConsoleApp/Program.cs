using System;
using Topshelf;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        static void Start()
        {
            HostFactory.Run(x =>
            {
                x.Service<WebServer>(s =>
                {
                    s.ConstructUsing(f => new WebServer());
                    s.WhenStarted(f => f.Start());
                    s.WhenStopped(f => f.Stop());
                });

                x.UseLog4Net(AppDomain.CurrentDomain.BaseDirectory + "log4net.config", true);

                x.RunAsLocalSystem();
                x.SetDescription("WebAPI SelfHost");
                x.SetDisplayName("WebAPI");
                x.SetServiceName("WebAPI SelfHost");
            });
        }
    }
}

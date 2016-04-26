using System;
using MS.Infrastructure;
using Topshelf;
using Topshelf.Autofac;

namespace MS.ProductAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            EngineContext.Initialize(false);

            HostFactory.Run(x =>
            {
                x.UseAutofacContainer(EngineContext.Current.ContainerManager.Container);
                x.Service<StartupService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.UseLog4Net(AppDomain.CurrentDomain.BaseDirectory + "log4net.config", true);
            });
        }
    }
}

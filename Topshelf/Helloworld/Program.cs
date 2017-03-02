using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Helloworld
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                //x.Service<TimerService>(s =>
                //{
                //    s.ConstructUsing(name => new TimerService());
                //    s.WhenStarted(tc => tc.Start());
                //    s.WhenStopped(tc => tc.Stop());
                //});

                x.Service<QuartzService>(s =>
                {
                    s.ConstructUsing(name => new QuartzService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalService();
                x.SetServiceName("测试服务");
                x.SetDisplayName("测试服务");
                x.SetDescription("测试服务");
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Topshelf;

namespace OrderAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<OrderService>(s =>
                {
                    s.ConstructUsing(y => new OrderService());
                    s.WhenStarted(y => y.Start());
                    s.WhenStopped(y => y.Stop());
                });
            });
        }
    }
}

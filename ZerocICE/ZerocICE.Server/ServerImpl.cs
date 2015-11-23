using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZerocICE.Server
{
    public class ServerImpl : Ice.Application
    {
        public static void Main(string[] args)
        {
            var app = new ServerImpl();
            int status = app.main(args, "config.server");
            if (status != 0)
            {
                System.Environment.Exit(status);
            }
        }

        public override int run(string[] args)
        {
            Ice.ObjectAdapter adapter = communicator().createObjectAdapter("SimplePrinter");
            adapter.add(new PrinterImpl(), communicator().stringToIdentity("SimplePrinter"));
            adapter.activate();
            communicator().waitForShutdown();

            return 0;
        }
    }
}

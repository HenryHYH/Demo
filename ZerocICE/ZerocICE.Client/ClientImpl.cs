using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZerocICE.Common;

namespace ZerocICE.Client
{
    public class ClientImpl : Ice.Application
    {
        public override int run(string[] args)
        {
            PrinterPrx printer = PrinterPrxHelper.checkedCast(communicator().propertyToProxy("Callback.CallbackServer"));

            if (printer == null)
            {
                System.Console.WriteLine("网络配置无效!");
                return 1;
            }

            string str = "Hello world";
            while (!string.IsNullOrEmpty(str))
            {
                printer.PrintString(str);
                str = Console.ReadLine();
            }

            return 0;
        }

        public static void Main(string[] args)
        {
            var app = new ClientImpl();
            int status = app.main(args, "config.client");
            if (0 != status)
                System.Environment.Exit(status);
        }
    }
}

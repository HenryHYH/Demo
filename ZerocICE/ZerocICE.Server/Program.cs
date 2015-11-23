using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZerocICE.Server
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    int status = 0;
        //    try
        //    {
        //        using (var ic = Ice.Util.initialize(ref args))
        //        {
        //            Ice.ObjectAdapter adapter = ic.createObjectAdapterWithEndpoints("SimplePrinter", "tcp -p 12345:udp -p 12345");
        //            Ice.Object obj = new PrinterImpl();
        //            adapter.add(obj, Ice.Util.stringToIdentity("SimplePrinter"));
        //            adapter.activate();
        //            ic.waitForShutdown();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.Error.WriteLine(e);
        //        status = 1;
        //    }
        //    Environment.Exit(status);
        //}
    }
}
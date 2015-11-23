using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZerocICE.Common;

namespace ZerocICE.Client
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    int status = 0;
        //    Ice.Communicator ic = null;
        //    try
        //    {
        //        ic = Ice.Util.initialize(ref args);
        //        Ice.ObjectPrx obj = ic.stringToProxy("SimplePrinter:tcp -p 12345:udp -p 12345");
        //        PrinterPrx printer = PrinterPrxHelper.checkedCast(obj);
        //        if (printer == null)
        //            throw new ApplicationException("Invalid proxy");

        //        string str = "Hello world";
        //        while (!string.IsNullOrEmpty(str))
        //        {
        //            printer.PrintString(str);
        //            str = Console.ReadLine();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.Error.WriteLine(e);
        //        status = 1;
        //    }
        //    if (ic != null)
        //    {
        //        // Clean up
        //        //
        //        try
        //        {
        //            ic.destroy();
        //        }
        //        catch (Exception e)
        //        {
        //            Console.Error.WriteLine(e);
        //            status = 1;
        //        }
        //    }
        //    Environment.Exit(status);
        //}
    }
}

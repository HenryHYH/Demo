using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ice;
using ZerocICE.Common;

namespace ZerocICE.Server
{
    public class PrinterImpl : PrinterDisp_
    {
        public override void PrintString(string s, Ice.Current current__)
        {
            Console.WriteLine(s);
        }
    }
}

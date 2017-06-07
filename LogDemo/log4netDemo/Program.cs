using log4net;
using System;

namespace log4netDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog Log = LogManager.GetLogger("SomeName");
            ILog SummaryLog = LogManager.GetLogger("Summary");
            Log.Debug("Processing");
            SummaryLog.Debug("Processing2");

            Console.WriteLine("Press any keys");
            Console.ReadKey();
        }
    }
}

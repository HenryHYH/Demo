using log4net;
using System;
using System.Threading.Tasks;

namespace log4netDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test();
            TestDynamic();

            Console.WriteLine("Press any keys");
            Console.ReadKey();
        }

        static void Test()
        {
            ILog Log = LogManager.GetLogger("SomeName");
            ILog SummaryLog = LogManager.GetLogger("Summary");
            Log.Debug("Processing");
            SummaryLog.Debug("Processing2");
        }

        static void TestDynamic()
        {
            var option = new ParallelOptions { MaxDegreeOfParallelism = 2 };
            Parallel.For(0, 100, option, (index) =>
            {
                DynamicLogHelper.Error("Client", "Test", "Message" + index);
            });
            DynamicLogHelper.Debug("Client", "Test", "Message");
        }
    }
}

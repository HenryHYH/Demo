﻿using log4net;
using System;
using System.Threading.Tasks;

namespace log4netDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test();
            //TestDynamic();
            //TestMulti();
            TestConfig();

            Console.WriteLine("Press any keys");
            Console.ReadKey();
        }

        static void TestConfig()
        {
            Parallel.For(0, 10000, new ParallelOptions { MaxDegreeOfParallelism = 100 }, (index) =>
            {
                TestConfigHelper.Debug("Debug" + index, "Debug");
            });
            TestConfigHelper.Debug("Debug", "Debug2");
            TestConfigHelper.Debug("DebugA", "Debug2");
            TestConfigHelper.Debug("Debug2", "Debug2");
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

        static void TestMulti()
        {
            var repository = LogManager.GetRepository();
            var appenders = repository.GetAppenders();
            var loggers = repository.GetCurrentLoggers();

            var option = new ParallelOptions { MaxDegreeOfParallelism = 10 };
            //Parallel.For(0, 100, option, (index) =>
            //{
            //    Debug("Debug", "Debug");
            //});
            Parallel.For(0, 100, option, (index) =>
            {
                Error("中文\\测试", "Error");
            });
        }

        private static void Debug(string name, object message)
        {
            var log = GetLog(name);
            log.Debug(message);
        }

        private static void Error(string name, object message)
        {
            var log = GetLog(name);
            log.Error(message);
        }

        private static ILog GetLog(string name)
        {
            return LogManager.GetLogger(name);
        }
    }
}

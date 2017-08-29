using HelloWeb.MessageSystem.Core.Domain.Logging;
using HelloWeb.MessageSystem.Core.Service;
using Quartz;
using System;
using System.Linq;

namespace HelloWeb.MessageSystem.ScheduleService
{
    [DisallowConcurrentExecution]
    public class TestJob : IJob
    {
        private readonly ILogService service;

        public TestJob(ILogService service)
        {
            this.service = service;
        }

        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff} - Start", DateTime.Now);

            service.Insert(new Log
            {
                ProjectName = "HelloWeb",
                AppName = "MessageSystem",
                Level = LogLevel.Debug,
                Message = "Hello world",
                Exception = new Exception("Hello Exception")
            });

            var list = service.Search("HelloWeb");
            var count = list.Count();
            Console.WriteLine($"Count = {count}");

            Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff} - End", DateTime.Now);
        }
    }
}

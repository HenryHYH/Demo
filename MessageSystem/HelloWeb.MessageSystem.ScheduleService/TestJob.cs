using HelloWeb.MessageSystem.Core.Service;
using Quartz;
using System;

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

            var count = service.ReceiveAndSave(16);
            Console.WriteLine($"Count = {count}");

            Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff} - End", DateTime.Now);
        }
    }
}

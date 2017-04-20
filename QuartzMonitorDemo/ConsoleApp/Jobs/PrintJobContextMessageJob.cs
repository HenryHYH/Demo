using Quartz;
using System;

namespace ConsoleApp.Jobs
{
    public class PrintJobContextMessageJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.ffff}", (context.NextFireTimeUtc ?? DateTime.MinValue).ToLocalTime());
        }
    }
}

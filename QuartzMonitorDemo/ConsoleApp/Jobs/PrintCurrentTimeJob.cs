using Quartz;
using System;

namespace ConsoleApp.Jobs
{
    [DisallowConcurrentExecution]
    public class PrintCurrentTimeJob : IJob
    {
        private static int Count = 0;
        private static bool triggerChanged = false;

        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("{1} - {2} - {0:yyyy-MM-dd HH:mm:ss.ffff}",
                                DateTime.Now,
                                Count,
                                triggerChanged ? 3 : 1);

            Count += 1;
            if (Count >= 5)
            {
                var trigger = TriggerBuilder.Create()
                                            .WithIdentity("JobTrigger")
                                            .WithCronSchedule(string.Format("0/{0} * * * * ?", triggerChanged ? 1 : 3))
                                            .Build();
                context.Scheduler.RescheduleJob(new TriggerKey("JobTrigger"), trigger);

                Count = 0;
                triggerChanged = !triggerChanged;
            }
        }
    }
}

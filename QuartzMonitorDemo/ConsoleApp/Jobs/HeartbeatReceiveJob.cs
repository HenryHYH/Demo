using ConsoleApp.Monitor;
using ConsoleApp.Utilities;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ConsoleApp.Jobs
{
    [DisallowConcurrentExecution]
    public class HeartbeatReceiveJob : IJob
    {
        private readonly string mqPath;
        private readonly MqHandler mqHandler;
        private readonly TimeSpan DelayTime = new TimeSpan(0, 0, 5);

        public HeartbeatReceiveJob()
        {
            mqPath = ConfigurationManager.AppSettings["ReceiveMqPath"];
            mqHandler = new MqHandler(mqPath);
        }

        public void Execute(IJobExecutionContext context)
        {
            var messages = mqHandler.Receive<MonitorMessage>();
            var lastestMessage = messages.OrderByDescending(x => x.CreateTime)
                                         .FirstOrDefault();

            if (null != lastestMessage)
            {
                Console.WriteLine("[{0:yyyy-MM-dd HH:mm:ss.ffff}] Receive:  {1}",
                                    DateTime.Now,
                                    lastestMessage.Message);

                var jobName = lastestMessage.InstanceName;
                context.Scheduler.DeleteJob(new JobKey(jobName));
                if (lastestMessage.NextFireTime.HasValue)
                {
                    var alarmTime = lastestMessage.NextFireTime.Value.Add(DelayTime);
                    var jobData = new Dictionary<string, object>
                    {
                        { "MonitorMessage", lastestMessage }
                    };

                    context.Scheduler.AddJob<HeartbeatAlarmJob>(alarmTime, jobName, jobData);
                }
            }
        }
    }
}

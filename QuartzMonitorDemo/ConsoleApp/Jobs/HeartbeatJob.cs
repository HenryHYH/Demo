using ConsoleApp.Monitor;
using ConsoleApp.Utilities;
using Quartz;
using System;
using System.Configuration;

namespace ConsoleApp.Jobs
{
    [DisallowConcurrentExecution]
    public class HeartbeatJob : IJob
    {
        private readonly MqHandler mqHandler;
        private readonly string instanceName;
        private readonly string mqPath;

        public HeartbeatJob()
        {
            instanceName = ConfigurationManager.AppSettings["InstanceName"];
            mqPath = ConfigurationManager.AppSettings["SendMqPath"];
            mqHandler = new MqHandler(mqPath);
        }

        public void Execute(IJobExecutionContext context)
        {
            var monitorMessage = new MonitorMessage
            {
                InstanceName = instanceName,
                ModuleName = "Helloworld",
                CreateTime = DateTime.Now,
                NextFireTime = context.NextFireTimeUtc.HasValue ?
                                context.NextFireTimeUtc.Value.DateTime.ToLocalTime() :
                                (DateTime?)null
            };
            mqHandler.Send(monitorMessage);
            Console.WriteLine("[{0:yyyy-MM-dd HH:mm:ss.ffff}] Send:     {1}",
                                DateTime.Now,
                                monitorMessage.Message);
        }
    }
}

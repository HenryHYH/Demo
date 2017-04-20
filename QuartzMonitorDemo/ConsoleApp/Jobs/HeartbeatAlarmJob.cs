using ConsoleApp.Monitor;
using Quartz;
using System;

namespace ConsoleApp.Jobs
{
    public class HeartbeatAlarmJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var job = context.JobDetail;
            var jobDataMap = job.JobDataMap;

            var monitorMessage = (MonitorMessage)context.JobDetail.JobDataMap["MonitorMessage"];

            if (null != monitorMessage)
                Console.WriteLine("{0} Alarm!!!", monitorMessage.InstanceName);
        }
    }
}

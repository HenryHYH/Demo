using System;
using System.Collections.Generic;
using System.Linq;

namespace Quartz
{
    public static class SchedulerExtension
    {
        public static Tuple<IJobDetail, ITrigger> AddJob<T>(this IScheduler scheduler,
                                                            string cronExpression,
                                                            string jobName = null)
            where T : IJob
        {
            jobName = jobName ?? typeof(T).Name;
            var triggerName = jobName + "Trigger";

            var job = JobBuilder.Create<T>()
                                .WithIdentity(jobName)
                                .Build();
            var trigger = TriggerBuilder.Create()
                                        .WithIdentity(triggerName)
                                        .WithCronSchedule(cronExpression)
                                        .StartAt(DateTime.Now)
                                        .Build();

            scheduler.ScheduleJob(job, trigger);

            return new Tuple<IJobDetail, ITrigger>(job, trigger);
        }

        public static Tuple<IJobDetail, ITrigger> AddJob<T>(this IScheduler scheduler,
                                                            DateTimeOffset startAt,
                                                            string jobName = null,
                                                            IDictionary<string, object> jobData = null)
            where T : IJob
        {
            jobName = jobName ?? typeof(T).Name;
            var triggerName = jobName + "Trigger";

            var jobBuilder = JobBuilder.Create<T>()
                                .WithIdentity(jobName);
            if (null != jobData && jobData.Any())
                jobBuilder.UsingJobData(new JobDataMap(jobData));

            var job = jobBuilder.Build();

            var trigger = TriggerBuilder.Create()
                                        .WithIdentity(triggerName)
                                        .StartAt(startAt)
                                        .Build();

            scheduler.ScheduleJob(job, trigger);

            return new Tuple<IJobDetail, ITrigger>(job, trigger);
        }
    }
}

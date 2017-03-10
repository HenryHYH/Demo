using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Helloworld.Jobs;
using Quartz.Impl;
using System.Collections.Specialized;

namespace Helloworld
{
    public class QuartzService
    {
        private ILog log = log4net.LogManager.GetLogger(typeof(QuartzService));
        private IScheduler scheduler = null;

        public QuartzService()
        {
            var properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "RemoteServerSchedulerClient";

            // 设置线程池
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "5";
            properties["quartz.threadPool.threadPriority"] = "Normal";

            // 远程输出配置
            properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
            properties["quartz.scheduler.exporter.port"] = "556";
            properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";
            properties["quartz.scheduler.exporter.channelType"] = "tcp";

            var schedulerFactory = new StdSchedulerFactory(properties);
            scheduler = schedulerFactory.GetScheduler();
        }

        private void Run()
        {
            IJobDetail job = JobBuilder.Create<ShowTimeJob>()
                                       .WithIdentity("MyJob", "Group1")
                                       .Build();

            ITrigger trigger = TriggerBuilder.Create()
                                        .WithIdentity("MyJobTrigger", "GroupA")
                                        //.StartAt(DateTime.Now.AddSeconds(5))
                                        //.WithSimpleSchedule(x => x.WithIntervalInSeconds(5)
                                        //                          .RepeatForever()
                                        //                    )
                                        .WithCronSchedule("0/3 * * ? * *")
                                        .StartNow()
                                        .Build();

            scheduler.ScheduleJob(job, trigger);

        }

        public void Start()
        {
            log.Debug("Start");

            scheduler.Start();
            Run();
        }

        public void Stop()
        {
            scheduler.Shutdown();
            log.Debug("Stop");
        }
    }
}

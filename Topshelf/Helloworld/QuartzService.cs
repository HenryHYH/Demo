using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Helloworld.Jobs;
using Quartz.Impl;

namespace Helloworld
{
    public class QuartzService
    {
        private ILog log = log4net.LogManager.GetLogger(typeof(QuartzService));
        private IScheduler scheduler = null;

        public QuartzService()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
        }

        private void Run()
        {
            IJobDetail job = JobBuilder.Create<ShowTimeJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                                        // .StartAt(DateTime.Now.AddSeconds(5))
                                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(5)
                                                                  .RepeatForever()
                                                                  //.WithMisfireHandlingInstructionIgnoreMisfires()
                                                            )
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

using ConsoleApp.Jobs;
using Quartz;
using Quartz.Impl;

namespace ConsoleApp
{
    public class MainService
    {
        private readonly IScheduler scheduler;

        public MainService()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
        }

        public void Start()
        {
            scheduler.AddJob<HeartbeatJob>("0/3 * * * * ?");
            scheduler.AddJob<HeartbeatReceiveJob>("1/1 * * * * ?");
            //scheduler.AddJob<PrintCurrentTimeJob>("0/3 * * * * ?");
            //scheduler.AddJob<PrintJobContextMessageJob>("0/3 * * * * ?");

            scheduler.Start();
        }

        public void Stop()
        {
            if (null != scheduler)
                scheduler.Shutdown();
        }
    }
}

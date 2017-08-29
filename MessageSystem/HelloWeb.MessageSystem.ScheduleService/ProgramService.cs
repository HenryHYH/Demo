using Quartz;
using Quartz.Impl;

namespace HelloWeb.MessageSystem.ScheduleService
{
    public class ProgramService
    {
        private readonly IScheduler scheduler;

        public ProgramService()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = new JobFactory(DependencyConfig.Container);
        }

        public void Start()
        {
            if (null != scheduler)
                scheduler.Start();
        }

        public void Stop()
        {
            if (null != scheduler)
                scheduler.Shutdown(false);
        }

        public void Pause()
        {
            if (null != scheduler)
                scheduler.ResumeAll();
        }

        public void Continue()
        {
            if (null != scheduler)
                scheduler.PauseAll();
        }
    }
}

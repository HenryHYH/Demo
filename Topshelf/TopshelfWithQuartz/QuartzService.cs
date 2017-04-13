using Quartz;
using Quartz.Impl;

namespace TopshelfWithQuartz
{
    public class QuartzService
    {
        private readonly IScheduler scheduler;

        public QuartzService()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
        }

        public void Start()
        {
            scheduler.Start();
        }

        public void Stop()
        {
            scheduler.Shutdown(false);
        }

        public void Continue()
        {
            scheduler.ResumeAll();
        }

        public void Pause()
        {
            scheduler.PauseAll();
        }
    }
}

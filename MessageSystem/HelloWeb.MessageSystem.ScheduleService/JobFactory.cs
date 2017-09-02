using Autofac;
using Quartz;
using Quartz.Spi;

namespace HelloWeb.MessageSystem.ScheduleService
{
    public class JobFactory : IJobFactory
    {
        private readonly IContainer container;

        public JobFactory(IContainer container)
        {
            this.container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)container.Resolve(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}

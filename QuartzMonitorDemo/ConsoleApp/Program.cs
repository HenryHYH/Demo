using Topshelf;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(c =>
            {
                c.Service<MainService>(s =>
                {
                    s.ConstructUsing(x => new MainService());
                    s.WhenStarted(x => x.Start());
                    s.WhenStopped(x => x.Stop());
                });

                c.RunAsLocalService();
                c.SetServiceName("ServiceName");
                c.SetDisplayName("Display Name");
                c.SetDescription("Description");
            });

            /*
             * Job.Heartbeat(instanceName, duration, currentTime);
             * 
             * function HeartbeatReceive(obj)
             * {
             *      Save(obj);
             *      
             *      timer.update();
             * }
             */
        }
    }
}

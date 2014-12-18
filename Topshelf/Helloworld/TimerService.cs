using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Helloworld
{
    public class TimerService
    {
        private readonly Timer timer;
        private ILog log = log4net.LogManager.GetLogger(typeof(TimerService));

        public TimerService()
        {
            timer = new Timer(1000) { AutoReset = true };
            timer.Elapsed += (sender, e) =>
            {
                log.DebugFormat("{0}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            };
        }

        public void Start()
        {
            log.Error("Start");
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}

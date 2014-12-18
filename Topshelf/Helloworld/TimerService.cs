using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Helloworld
{
    public class TimerService
    {
        private readonly Timer timer;

        public TimerService()
        {
            timer = new Timer(1000) { AutoReset = true };
            timer.Elapsed += (sender, e) =>
            {
                Debug.WriteLine("Time is {0}.", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", System.Globalization.DateTimeFormatInfo.CurrentInfo));
                Console.WriteLine("Time is {0}.", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", System.Globalization.DateTimeFormatInfo.CurrentInfo));
            };
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}

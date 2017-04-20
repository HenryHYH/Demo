using System;

namespace ConsoleApp.Monitor
{
    public class MonitorMessage
    {
        public DateTime CreateTime { get; set; }

        public string InstanceName { get; set; }

        public string ModuleName { get; set; }

        public string Message
        {
            get
            {
                return string.Format("{1}.{2} at {0:yyyy-MM-dd HH:mm:ss.ffff}, NextFireTime = [{3:yyyy-MM-dd HH:mm:ss.ffff}]",
                                     CreateTime,
                                     InstanceName,
                                     ModuleName,
                                     (NextFireTime ?? DateTime.MinValue).ToLocalTime());
            }
        }

        public DateTime? NextFireTime { get; set; }
    }
}

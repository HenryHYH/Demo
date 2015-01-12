using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Core.Domain.Logging
{
    public class Log : BaseEntity
    {
        public LogLevel LogLevel { get; set; }

        public string ShortMessage { get; set; }

        public string FullMessage { get; set; }

        public DateTime CreateTime { get; set; }
    }
}

using System;

namespace HelloWeb.MessageSystem.Core.Domain.Logging
{
    public class Log : BaseEntity
    {
        public string ProjectName { get; set; }

        public string AppName { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public LogLevel Level { get; set; }
    }
}

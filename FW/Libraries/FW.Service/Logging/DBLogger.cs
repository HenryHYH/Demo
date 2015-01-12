using FW.Core.Data;
using FW.Core.Domain.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Service.Logging
{
    public class DBLogger : ILogger
    {
        private readonly IRepository<Log> logRepository;

        public DBLogger(IRepository<Log> logRepository)
        {
            this.logRepository = logRepository;
        }

        public Log Insert(LogLevel level, string shortMessage, string fullMessage = "")
        {
            Log log = new Log()
            {
                LogLevel = level,
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                CreateTime = DateTime.Now
            };

            logRepository.Insert(log);

            return log;
        }
    }
}

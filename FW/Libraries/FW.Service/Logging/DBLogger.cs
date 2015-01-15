namespace FW.Service.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FW.Core.Data;
    using FW.Core.Domain.Logging;

    public class DBLogger : ILogger
    {
        #region Fields

        private readonly IRepository<Log> logRepository;

        #endregion Fields

        #region Constructors

        public DBLogger(IRepository<Log> logRepository)
        {
            this.logRepository = logRepository;
        }

        #endregion Constructors

        #region Methods

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

        #endregion Methods
    }
}
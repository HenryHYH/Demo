namespace FW.Service.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FW.Core.Domain.Logging;

    public static class LoggerExtensions
    {
        #region Methods

        public static void Debug(this ILogger logger, string message, Exception ex = null)
        {
            WriteLog(logger, LogLevel.Debug, message, ex);
        }

        public static void Error(this ILogger logger, string message, Exception ex = null)
        {
            WriteLog(logger, LogLevel.Error, message, ex);
        }

        public static void Info(this ILogger logger, string message, Exception ex = null)
        {
            WriteLog(logger, LogLevel.Info, message, ex);
        }

        public static void Warning(this ILogger logger, string message, Exception ex = null)
        {
            WriteLog(logger, LogLevel.Warning, message, ex);
        }

        private static void WriteLog(ILogger logger, LogLevel level, string message, Exception ex)
        {
            string fullMessage = null == ex ? string.Empty : ex.ToString();
            logger.Insert(level, message, fullMessage);
        }

        #endregion Methods
    }
}
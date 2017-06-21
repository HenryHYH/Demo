using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace log4netDemo
{
    public class TestConfigHelper
    {
        private static readonly IDictionary<string, ILog> LOGGERS = new Dictionary<string, ILog>();
        private static readonly object locker = new object();

        private static ILog Config(string dir)
        {
            ILog log = null;
            var exists = LOGGERS.TryGetValue(dir, out log);
            if (exists)
                return log;

            lock (locker)
            {
                log = LogManager.GetLogger(dir);
                var hierarchy = (Hierarchy)log.Logger.Repository;
                if (hierarchy.GetAppenders().Any(s => s.Name == dir))
                    return log;

                var baseFilePath = @"D:\Log\";
                var file = Path.Combine(baseFilePath, dir);
                if (!file.EndsWith("\\"))
                    file += "\\";

                var patternLayout = new PatternLayout();
                patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
                patternLayout.ActivateOptions();

                var roller = new RollingFileAppender();
                roller.File = file;
                roller.AppendToFile = true;
                roller.MaxSizeRollBackups = -1;
                roller.MaximumFileSize = "100MB";
                roller.Encoding = Encoding.UTF8;
                roller.RollingStyle = RollingFileAppender.RollingMode.Composite;
                roller.DatePattern = "yyyy-MM-dd\".log\"";
                roller.StaticLogFileName = false;
                roller.Layout = patternLayout;
                roller.Name = dir;
                roller.ActivateOptions();

                ((Logger)log.Logger).AddAppender(roller);

                hierarchy.Configured = true;

                LOGGERS.Add(dir, log);
                return log;
            }
        }

        public static void Debug(string message, string dir)
        {
            var log = Config(dir);
            log.Debug(message);
        }

        public static void Error(string message, string dir)
        {
            var log = LogManager.GetLogger(dir);
            log.Error(message);
        }
    }
}

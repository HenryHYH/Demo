using log4net;
using System.Collections.Generic;

namespace log4netDemo
{
    public class DynamicLogHelper
    {
        //将日记对象缓存起来
        public static Dictionary<string, ILog> LogDic = new Dictionary<string, ILog>();
        static object _islock = new object();
        public static ILog GetLog(string name)
        {
            try
            {
                if (LogDic == null)
                {
                    LogDic = new Dictionary<string, ILog>();
                }
                lock (_islock)
                {
                    if (!LogDic.ContainsKey(name))
                    {
                        LogDic.Add(name, LogManager.GetLogger(name));
                    }
                }
                return LogDic[name];
            }
            catch
            {
                return LogManager.GetLogger("Default");
            }
        }

        public static void Debug(string client, string name, object message)
        {
            //该参数有三部分组成：客户端_日记类型_logger配置名称；<file value="logs/%Client/%LogType/" />
            //value如果不需要客户端时可写成<file value="logs/%LogType/" />；这里是需要动态配置的才加上
            //在上面的配置，name传入的就是 Test 最终字符串是：客户端_Debug_Test
            //var log = GetLog(string.Format("{0}_Debug_{1}", client, name));
            //if (log == null)
            //{
            //    return;
            //}

            //ThreadContext.Properties["logFileName"] = string.Format("{0}_Debug_{1}", client, name);
            var log = LogManager.GetLogger(name);


            var h = (log4net.Repository.Hierarchy.Hierarchy)log.Logger.Repository;
            foreach (log4net.Appender.IAppender a in h.GetAppenders())
            {
                if (a.Name == "LogFileAppender")
                {
                    log4net.Appender.FileAppender fa = (log4net.Appender.FileAppender)a;

                    fa.File = string.Format(@"D:\{0}\", @"DEBUG");
                    fa.ActivateOptions();
                }
            }



            log.Debug(message);
        }
        public static void Error(string client, string name, object message)
        {
            //var log = GetLog(string.Format("{0}_Error_{1}", client, name));
            //if (log == null)
            //{
            //    return;
            //}

            //ThreadContext.Properties["logFileName"] = string.Format("{0}_Error_{1}", client, name);
            var log = LogManager.GetLogger(name);


            var h = (log4net.Repository.Hierarchy.Hierarchy)log.Logger.Repository;

            var list = LogManager.GetRepository().GetAppenders();


            foreach (log4net.Appender.IAppender a in h.GetAppenders())
            {
                if (a.Name == "LogFileAppender")
                {
                    log4net.Appender.RollingFileAppender fa = (log4net.Appender.RollingFileAppender)a;

                    fa.File = string.Format(@"D:\{0}\", @"DEBUG\ERROR");
                    fa.ActivateOptions();
                }
            }

            log.Error(message);
        }
    }
}

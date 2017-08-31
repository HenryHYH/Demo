using System;

namespace HelloWeb.MessageSystem.WebApi.Models.Log
{
    /// <summary>
    /// 日志信息
    /// </summary>
    public class LogModel
    {
        /// <summary>
        /// <see cref="LogModel"/>
        /// </summary>
        public LogModel()
        {
            UniqueSequence = Guid.NewGuid().ToString();
            Level = LogLevel.Info;
            CTime = DateTime.Now;
        }

        /// <summary>
        /// 跟踪流水
        /// </summary>
        public string UniqueSequence { get; set; }

        /// <summary>
        /// 日志信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CTime { get; set; }
    }
}
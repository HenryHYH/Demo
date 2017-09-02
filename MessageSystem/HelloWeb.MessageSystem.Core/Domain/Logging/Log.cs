using System;

namespace HelloWeb.MessageSystem.Core.Domain.Logging
{
    public class Log : BaseEntity
    {
        /// <summary>
        /// 跟踪流水
        /// </summary>
        public string UniqueSequence { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }

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
        public ExceptionMessage Exception { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CTime { get; set; }
    }
}

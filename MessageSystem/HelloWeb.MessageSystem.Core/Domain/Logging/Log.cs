using System;
using System.Collections.Generic;

namespace HelloWeb.MessageSystem.Core.Domain.Logging
{
    /// <summary>
    /// 日志信息
    /// </summary>
    public class Log : BaseEntity
    {
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
        public string ShortMessage { get; set; }

        /// <summary>
        /// 日志异常详细信息
        /// </summary>
        public string FullMessage { get; set; }

        /// <summary>
        /// 日志等级ID
        /// </summary>
        public int LogLevelId { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public IDictionary<string, string> Extended { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}

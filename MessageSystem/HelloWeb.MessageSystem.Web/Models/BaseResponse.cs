using System;

namespace HelloWeb.MessageSystem.WebApi.Models
{
    /// <summary>
    /// 响应信息
    /// </summary>
    public abstract class BaseResponse
    {
        /// <summary>
        /// <see cref="BaseResponse"/>
        /// </summary>
        public BaseResponse()
        {
            Success = false;
            ErrorMessage = string.Empty;
            ResponseTime = DateTime.Now;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 响应时间
        /// </summary>
        public DateTime ResponseTime { get; set; }
    }
}
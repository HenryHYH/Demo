using HelloWeb.MessageSystem.WebApi.Models.Log;
using System.Collections.Generic;
using System.Web.Http;

namespace HelloWeb.MessageSystem.WebApi.Controllers
{
    /// <summary>
    /// 日志API
    /// </summary>
    public class LogController : ApiController
    {
        /// <summary>
        /// 获取日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "A", "B", "C" };
        }

        /// <summary>
        /// 提交日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public LogResponse Post(LogModel request)
        {
            return new LogResponse
            {
                Success = true
            };
        }
    }
}

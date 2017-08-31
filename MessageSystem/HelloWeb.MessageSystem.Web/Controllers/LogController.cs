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
    }
}

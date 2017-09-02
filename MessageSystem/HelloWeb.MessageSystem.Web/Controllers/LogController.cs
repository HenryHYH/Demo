using AutoMapper;
using HelloWeb.MessageSystem.Core.Domain.Logging;
using HelloWeb.MessageSystem.Core.Service;
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
        private readonly ILogService logService;

        /// <summary>
        /// <see cref="LogController"/>
        /// </summary>
        public LogController(ILogService logService)
        {
            this.logService = logService;
        }

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
            var log = Mapper.Map<LogModel, Log>(request);
            var success = logService.Send(log);

            return new LogResponse
            {
                Success = success
            };
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace WebApp.Controllers
{
    public class LogController : Controller
    {
        private ILogger<LogController> logger;

        public LogController(ILogger<LogController> logger)
        {
            this.logger = logger;
        }

        public string Index()
        {
            logger.LogTrace(1000, "TRACE");
            logger.LogDebug(1001, "DEBUG");
            logger.LogInformation(1002, "INFO");
            logger.LogWarning(1003, "WARNING");
            logger.LogError(1004, "ERROR");
            logger.LogCritical(1005, "CRITICAL");

            return DateTime.Now.ToString();
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WebApp.Filters
{
    public class LoggingFilter : IActionFilter
    {
        private ILogger<LoggingFilter> logger;

        public LoggingFilter(ILogger<LoggingFilter> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //logger.LogInformation("OnActionExecuting");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //logger.LogInformation("OnActionExecuted");
        }
    }
}

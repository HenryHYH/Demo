using Microsoft.Extensions.Logging;

namespace WebApp.Services
{
    public class LogService : ILogService
    {
        private readonly ILogger<LogService> logger;

        public LogService(ILogger<LogService> logger)
        {
            this.logger = logger;
        }

        public void Info(string message)
        {
            logger.LogInformation(message);
        }
    }
}

using Microsoft.Extensions.Logging;

namespace WebApp.Services
{
    public class CustomService : ICustomService
    {
        private readonly ILogger logger;

        public CustomService(ILogger<CustomService> logger)
        {
            this.logger = logger;
        }

        public void Call()
        {
            logger.LogDebug("Call impl");
        }
    }
}

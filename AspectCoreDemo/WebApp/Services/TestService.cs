using Microsoft.Extensions.Logging;

namespace WebApp.Services
{
    public class TestService : ITestService
    {
        private readonly ILogger logger;

        public TestService(ILogger<TestService> logger)
        {
            this.logger = logger;
        }

        public void Call()
        {
            logger.LogDebug("Call impl");
        }
    }
}

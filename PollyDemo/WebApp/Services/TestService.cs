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

        public void Say(string message)
        {
            logger.LogDebug($"Say {message}");

            throw new System.Exception("CustomException");
        }

        public void SayFallBack(string message)
        {
            logger.LogError($"Say Method Error, Logging: {message}");
        }
    }
}

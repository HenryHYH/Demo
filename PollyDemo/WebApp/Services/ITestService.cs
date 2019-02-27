using WebApp.Infrastructures.Interceptors;

namespace WebApp.Services
{
    public interface ITestService
    {
        [PollyCommand(nameof(SayFallBack),
            EnableCircuitBreaker = true,
            ExceptionsAllowedBeforeBreaking = 3,
            MillisecondsOfBreak = 1000 * 5)]
        void Say(string message);

        void SayFallBack(string message);
    }
}

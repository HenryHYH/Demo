using WebApp.Infrastructure;

namespace WebApp.Services
{
    public interface ICustomService
    {
        [CustomInterceptor]
        void Call();
    }
}

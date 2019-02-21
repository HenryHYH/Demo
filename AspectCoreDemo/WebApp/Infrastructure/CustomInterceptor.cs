using AspectCore.DynamicProxy;
using AspectCore.Injector;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebApp.Infrastructure
{
    public class CustomInterceptor : AbstractInterceptor
    {
        [FromContainer]
        public ILogger<CustomInterceptor> Logger { get; set; }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                Logger.LogDebug("Before invoke");
                await next(context);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Exception is {ex}");
                throw;
            }
            finally
            {
                Logger.LogDebug("After inovke");
            }
        }
    }
}

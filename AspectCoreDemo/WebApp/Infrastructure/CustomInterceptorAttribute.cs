using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebApp.Infrastructure
{
    public class CustomInterceptorAttribute : AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var logger = context.ServiceProvider.GetService<ILogger<CustomInterceptorAttribute>>();

            try
            {
                logger.LogDebug("Before invoke");
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception is {ex}");
                throw;
            }
            finally
            {
                logger.LogDebug("After inovke");
            }
        }
    }
}

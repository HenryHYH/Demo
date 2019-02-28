using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HealthChecksDemo
{
    public class ExampleHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var healthy = DateTime.Now.Second % 3 != 0;

            if (healthy)
                return Task.FromResult(HealthCheckResult.Healthy("Healthy"));

            return Task.FromResult(HealthCheckResult.Unhealthy("Unhealthy"));
        }
    }
}

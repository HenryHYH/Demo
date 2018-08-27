using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApp.Infrastructure;

namespace WebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        private static int count = 0;
        private readonly IOptions<ConsulConfig> consulConfig;

        public CounterController(IOptions<ConsulConfig> consulConfig)
        {
            this.consulConfig = consulConfig;
        }

        [HttpGet]
        public string Count()
        {
            return $"Count {++count} from {consulConfig.Value.ServiceId}-{BindingHostedService.Uri.Port}";
        }
    }
}
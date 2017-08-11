using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApp.Configs;

namespace WebApp.Controllers
{
    public class ConfigController : Controller
    {
        private IOptions<CustomConfig> customConfig;

        public ConfigController(IOptions<CustomConfig> customConfig)
        {
            this.customConfig = customConfig;
        }

        public IActionResult Index()
        {
            return Ok(customConfig.Value);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private IConfiguration configuration;

        public ConfigController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("{key}")]
        public IActionResult Get(string key)
        {
            var value = configuration.GetValue(key, "NULL");

            return Content(string.Format("Value = {0}", value));
        }
    }
}
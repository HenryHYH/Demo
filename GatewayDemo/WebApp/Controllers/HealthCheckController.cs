using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet("")]
        [HttpHead("")]
        public IActionResult Ping() => Ok();
    }
}
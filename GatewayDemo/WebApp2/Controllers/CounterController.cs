using Microsoft.AspNetCore.Mvc;

namespace WebApp2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        private static int count = 0;

        [HttpGet]
        public string Count()
        {
            return $"Count {++count} from WebApi2";
        }
    }
}
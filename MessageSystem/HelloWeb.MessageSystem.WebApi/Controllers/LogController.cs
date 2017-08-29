using System.Collections.Generic;
using System.Web.Http;

namespace HelloWeb.MessageSystem.WebApi.Controllers
{
    public class LogController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "A", "B", "C" };
        }
    }
}

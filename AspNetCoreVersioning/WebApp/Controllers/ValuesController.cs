using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApp.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("3.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion("1.0")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "V1", HttpContext.GetRequestedApiVersion().ToString() };
        }

        [HttpGet]
        [MapToApiVersion("3.0")]
        public ActionResult<IEnumerable<string>> Getv3()
        {
            return new string[] { "V3", HttpContext.GetRequestedApiVersion().ToString() };
        }
    }
}

namespace WebApp.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/[controller]")] // http://localhost:5000/api/values?api-version=2.0
    [Route("api/v{version:apiVersion}/[controller]")] // http://localhost:5000/api/v2/values
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "V2", HttpContext.GetRequestedApiVersion().ToString() };
        }
    }
}

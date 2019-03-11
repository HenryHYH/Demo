using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "WebApi2", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") };
        }
    }
}

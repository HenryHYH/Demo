using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IEasyCachingProvider cachingProvider;

        public ValuesController(IEasyCachingProvider cachingProvider)
        {
            this.cachingProvider = cachingProvider;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var value = string.Empty;
            var cachedValue = cachingProvider.Get<string>("TestKey");
            if (cachedValue.HasValue)
                value = cachedValue.Value;
            else
            {
                value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff");
                cachingProvider.Set("TestKey", value, TimeSpan.FromSeconds(3));
            }

            return new string[] { "GET", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), value };
        }
    }
}

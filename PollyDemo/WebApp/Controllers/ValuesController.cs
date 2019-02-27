using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly ITestService testService;

        public ValuesController(
            ILogger<ValuesController> logger,
            ITestService testService)
        {
            this.logger = logger;
            this.testService = testService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            logger.LogDebug("Start Get");

            testService.Say("Hello Henry");

            var message = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}";

            return new string[] { message };
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly SelfService selfService;

        public ValuesController(
            IUserService userService,
            SelfService selfService)
        {
            this.userService = userService;
            this.selfService = selfService;
        }

        public ActionResult<string> Get()
        {
            var name = userService.GetName();
            var message = selfService.GetMessage();

            return $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}] - {name} - {message}";
        }
    }
}
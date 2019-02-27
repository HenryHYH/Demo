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

        public ValuesController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult<string> Get()
        {
            var name = userService.GetName();

            return $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}] - {name}";
        }
    }
}
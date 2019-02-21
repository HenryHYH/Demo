using Microsoft.AspNetCore.Mvc;
using System;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomService customService;
        private readonly ITestService testService;

        public HomeController(ICustomService customService,
            ITestService testService)
        {
            this.customService = customService ?? throw new ArgumentNullException(nameof(customService));
            this.testService = testService ?? throw new ArgumentNullException(nameof(testService));
        }

        public IActionResult Index()
        {
            customService.Call();
            testService.Call();

            return Content($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");
        }
    }
}
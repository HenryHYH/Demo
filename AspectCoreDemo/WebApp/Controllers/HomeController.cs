using Microsoft.AspNetCore.Mvc;
using System;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomService customService;

        public HomeController(ICustomService customService)
        {
            this.customService = customService ?? throw new ArgumentNullException(nameof(customService));
        }

        public IActionResult Index()
        {
            customService.Call();

            return Content($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");
        }
    }
}
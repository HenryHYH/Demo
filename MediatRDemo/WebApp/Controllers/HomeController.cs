using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApp.Infrastructure;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator mediator;

        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> TestRequest()
        {
            var res = await mediator.Send(new RequestPing());

            return Content($"[{DateTime.Now.ToString()}] - {res}.");
        }

        public async Task<IActionResult> TestPublish()
        {
            await mediator.Publish(new NotificationPing());

            return Content($"[{DateTime.Now.ToString()}]");
        }
    }
}

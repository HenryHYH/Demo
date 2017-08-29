using HelloWeb.MessageSystem.Core.Service;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HelloWeb.MessageSystem.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogService service;

        public HomeController(ILogService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return Content(string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}, Count = {1}", DateTime.Now, service.Search("").Count()));
        }
    }
}
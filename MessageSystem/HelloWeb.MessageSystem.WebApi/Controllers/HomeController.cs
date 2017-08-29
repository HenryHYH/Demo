using System;
using System.Web.Mvc;

namespace HelloWeb.MessageSystem.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content(string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now));
        }
    }
}
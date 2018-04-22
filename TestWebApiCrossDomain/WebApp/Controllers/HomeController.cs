using System;
using System.Web.Mvc;
using WebApp.Infrastructure;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ApiPermissionFilter]
        public ActionResult Post()
        {
            var url = Request.UrlReferrer.GetLeftPart(UriPartial.Authority);

            //Response.Headers.Remove("Access-Control-Allow-Origin");
            //Response.AddHeader("Access-Control-Allow-Origin", url);

            //Response.Headers.Remove("Access-Control-Allow-Credentials");
            //Response.AddHeader("Access-Control-Allow-Credentials", "true");

            //Response.Headers.Remove("Access-Control-Allow-Methods");
            //Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");

            return Json(new
            {
                Success = true,
                ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                Url = url
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
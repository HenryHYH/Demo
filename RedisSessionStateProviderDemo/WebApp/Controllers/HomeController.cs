using System;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Session.Timeout = 10;
            var val = Session["Helloworld"];
            if (null == val)
            {
                Session.Add("Helloworld", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.DateTimeFormatInfo.InvariantInfo));
                ViewBag.Value = "NULL";
            }
            else
                ViewBag.Value = val.ToString();

            Session["ABC"] = DateTime.Now.ToString();

            return View();
        }
    }
}
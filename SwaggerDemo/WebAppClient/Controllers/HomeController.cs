using System;
using System.Web.Mvc;
using WebApi;

namespace WebAppClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var client = new WebApiClient(new Uri("http://localhost:51726/"), new AnonymousCredential());
            var result = client.Values.Get();

            var user = client.User.PostByvalue("ABC");
            result.Add(user);

            ViewBag.Message = string.Join(", ", result);

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
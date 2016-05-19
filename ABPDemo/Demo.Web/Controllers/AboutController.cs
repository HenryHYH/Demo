using System.Web.Mvc;

namespace Demo.Web.Controllers
{
    public class AboutController : DemoControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
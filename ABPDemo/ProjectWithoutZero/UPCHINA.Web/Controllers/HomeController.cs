using System.Web.Mvc;

namespace UPCHINA.Web.Controllers
{
    public class HomeController : UPCHINAControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
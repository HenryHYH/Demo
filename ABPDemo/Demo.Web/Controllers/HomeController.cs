using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace Demo.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : DemoControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
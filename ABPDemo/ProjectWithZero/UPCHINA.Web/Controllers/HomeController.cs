using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace UPCHINA.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : UPCHINAControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
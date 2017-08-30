using System.Web.Mvc;

namespace HelloWeb.MessageSystem.WebApi.Controllers
{
    /// <summary>
    /// 系统主页
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
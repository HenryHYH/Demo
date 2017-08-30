using System.Web.Mvc;

namespace HelloWeb.MessageSystem.WebApi.Controllers
{
    /// <summary>
    /// 日志查看
    /// </summary>
    [RoutePrefix("log")]
    public class LoggingController : Controller
    {
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        [Route]
        public ActionResult Index()
        {
            return View();
        }
    }
}
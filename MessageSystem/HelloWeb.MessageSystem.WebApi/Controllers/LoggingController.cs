using System.Web.Mvc;

namespace HelloWeb.MessageSystem.WebApi.Controllers
{
    /// <summary>
    /// 日志查看
    /// </summary>
    [Route("log")]
    public class LoggingController : Controller
    {
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return Content("Logging");
        }
    }
}
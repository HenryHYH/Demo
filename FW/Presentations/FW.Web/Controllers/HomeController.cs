using FW.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FW.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly IUserService userService;

        #endregion

        #region Ctor

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        #endregion

        #region Mehtods

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

        public ActionResult Test()
        {
            var list = userService.GetUsers();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
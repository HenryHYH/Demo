using FW.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FW.Admin.Controllers
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test2()
        {
            return Json(userService.GetUsers(), JsonRequestBehavior.AllowGet);
        }
    }
}
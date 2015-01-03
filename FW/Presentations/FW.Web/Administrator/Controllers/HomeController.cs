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
        private readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return Json(userService.GetUsers(pageSize: 1), JsonRequestBehavior.AllowGet);
        }
    }
}
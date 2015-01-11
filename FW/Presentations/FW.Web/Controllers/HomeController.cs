namespace FW.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using FW.Service.Users;

    public class HomeController : Controller
    {
        #region Fields

        private readonly IUserService userService;

        #endregion Fields

        #region Constructors

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        #endregion Constructors

        #region Methods

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return Json(userService.GetUsers(), JsonRequestBehavior.AllowGet);
        }

        #endregion Methods
    }
}
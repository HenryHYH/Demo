namespace FW.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using FW.Service.Users;
    using FW.Web.Framework.Controllers;

    public class HomeController : BaseController
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
            return Json(userService.GetUsers(pageSize: 1), JsonRequestBehavior.AllowGet);
        }

        #endregion Methods
    }
}
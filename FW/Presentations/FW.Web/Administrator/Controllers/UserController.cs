namespace FW.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using FW.Admin.Models;
    using FW.Core.Domain.Users;
    using FW.Service.Users;
    using FW.Web.Framework.Controllers;
    using FW.Web.Framework.Datasource;
    using FW.Web.Framework.Extensions;

    public class UserController : BaseController
    {
        #region Fields

        private readonly IUserService userService;

        #endregion Fields

        #region Constructors

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        #endregion Constructors

        #region Methods

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserList(DatasourceRequest command)
        {
            var data = userService.GetUsers(
                pageIndex: command.PageIndex,
                pageSize: command.PageSize)
                .ToModel<User, UserModel>();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion Methods
    }
}
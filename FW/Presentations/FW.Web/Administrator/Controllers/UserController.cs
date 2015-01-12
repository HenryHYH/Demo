﻿using FW.Service.Users;
using FW.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FW.Web.Framework.Extensions;
using FW.Core.Domain.Users;
using FW.Admin.Models;

namespace FW.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserList()
        {
            var data = userService.GetUsers().ToResult<User, UserModel>();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
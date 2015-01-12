using FW.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FW.Admin.Controllers
{
    public class UserController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserList()
        {
            var data = new[] { 
                new { Index = 1, Name = "Henry", Age = 28 }, 
                new { Index = 2, Name = "Hello world", Age = 200 } 
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
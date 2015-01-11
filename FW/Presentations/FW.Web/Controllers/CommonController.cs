using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FW.Web.Controllers
{
    public class CommonController : Controller
    {
        [ChildActionOnly]
        public ActionResult Navigation()
        {
            return PartialView();
        }
    }
}
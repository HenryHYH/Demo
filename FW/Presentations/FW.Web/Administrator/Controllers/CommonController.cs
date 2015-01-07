using FW.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FW.Admin.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult Navigation()
        {
            var adminNode = new XmlSiteMap(Server.MapPath("~/Administrator/Web.sitemap")).Node;

            return PartialView(adminNode);
        }
    }
}
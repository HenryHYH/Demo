using FW.Admin.Models.Common;
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
            var siteMap = new XmlSiteMap(Server.MapPath("~/Administrator/Web.sitemap"));

            var list = new List<NavigationModel>();

            list.Add(new NavigationModel() { Title = "A", Url = "/", Description = "A_D" });
            list[0].Children.Add(new NavigationModel() { Title = "A_1", Url = "/", Description = "A_1_D" });

            return PartialView(list);
        }
    }
}
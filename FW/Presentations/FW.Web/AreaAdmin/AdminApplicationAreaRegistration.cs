using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FW.Admin
{
    public class AdminApplicationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "AreaAdmin"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", area = "AreaAdmin", id = UrlParameter.Optional },
                new[] { "FW.Admin.Controllers" }
            );
        }
    }
}
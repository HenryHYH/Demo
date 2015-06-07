namespace FW.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class AdminAreaRegistration : AreaRegistration
    {
        #region Properties

        public override string AreaName
        {
            get
            {
                return "Administrator";
            }
        }

        #endregion Properties

        #region Methods

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", area = "Admin", id = UrlParameter.Optional },
                new[] { "FW.Admin.Controllers" }
            );

            context.MapRoute("Admin_Content",
                "Admin/Content/");
        }

        #endregion Methods
    }
}
namespace FW.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using FW.Core.Infrastructure;
    using FW.Web.Framework.Controllers;
    using FW.Web.Framework.UI;
    using FW.Web.Framework.UI.Menu;
    using FW.Web.Framework.UI.Pagination;

    public class CommonController : BaseController
    {
        #region Fields

        private readonly Settings settings;

        #endregion Fields

        #region Constructors

        public CommonController(Settings settings)
        {
            this.settings = settings;
        }

        #endregion Constructors

        #region Methods

        [ChildActionOnly]
        public ActionResult Navigation()
        {
            var adminNode = new XmlSiteMap(Server.MapPath(settings.GetSetting("AdminSiteMapPath"))).Node;

            return PartialView(adminNode);
        }

        [ChildActionOnly]
        public ActionResult Pagination(int pageIndex, int totalRecords, int pageSize = 20)
        {
            var pager = new Pager();
            pager.TotalRecords = totalRecords;

            return PartialView(pager);
        }

        [ChildActionOnly]
        public ActionResult Table(string url)
        {
            return PartialView();
        }

        #endregion Methods
    }
}
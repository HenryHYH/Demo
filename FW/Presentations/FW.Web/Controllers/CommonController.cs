namespace FW.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class CommonController : Controller
    {
        #region Methods

        [ChildActionOnly]
        public ActionResult Navigation()
        {
            return PartialView();
        }

        #endregion Methods
    }
}
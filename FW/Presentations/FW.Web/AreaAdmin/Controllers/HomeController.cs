using FW.Service.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FW.Admin.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly ILocalizationService localizationService;

        #endregion

        #region Ctor

        public HomeController(ILocalizationService localizationService)
        {
            this.localizationService = localizationService;
        }

        #endregion

        #region Methods

        public ActionResult Index()
        {
            var resource = localizationService.GetResource(4);
            resource.ResourceValue = "DEF";
            localizationService.UpdateResource(resource);

            return View();
        }

        #endregion
    }
}
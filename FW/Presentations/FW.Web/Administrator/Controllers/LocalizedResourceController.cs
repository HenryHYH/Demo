using FW.Service.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FW.Web.Framework.Extensions;
using FW.Core.Domain.Localization;
using FW.Admin.Models;

namespace FW.Admin.Controllers
{
    public class LocalizedResourceController : Controller
    {
        private readonly ILocalizationService localizationService;

        public LocalizedResourceController(ILocalizationService localizationService)
        {
            this.localizationService = localizationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            var model = new LocalizedResourceModel();
            PrepareResourceModel(model, null);

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(LocalizedResourceModel model, FormCollection form)
        {
            var entity = model.MapTo<LocalizedResourceModel, LocalizedResource>();
            localizationService.InsertResource(entity);

            PrepareResourceModel(model, null);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = new LocalizedResourceModel();
            var entity = localizationService.GetResource(id);
            PrepareResourceModel(model, entity);

            return View();
        }

        [HttpGet]
        public ActionResult ResourceList()
        {
            var list = localizationService.GetResources()
                .ToModel<LocalizedResource, LocalizedResourceModel>();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        private void PrepareResourceModel(LocalizedResourceModel model, LocalizedResource entity)
        {
            if (null != entity)
            {
                model = entity.MapTo(model);
            }
        }
    }
}
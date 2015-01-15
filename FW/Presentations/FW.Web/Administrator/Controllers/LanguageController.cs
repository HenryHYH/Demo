using FW.Core.Domain.Localization;
using FW.Service.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FW.Web.Framework.Extensions;
using FW.Admin.Models;

namespace FW.Admin.Controllers
{
    public class LanguageController : Controller
    {
        private readonly ILocalizationService localizationService;

        public LanguageController(ILocalizationService localizationService)
        {
            this.localizationService = localizationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            var model = new LanguageModel();
            PrepareLanguageModel(model, null);

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(LanguageModel model, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var entity = model.MapTo<LanguageModel, Language>();
                localizationService.InsertLanguage(entity);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult LanguageList(int pageIndex, int pageSize)
        {
            var list = localizationService.GetLanguages(pageIndex, pageSize)
                                            .ToModel<Language, LanguageModel>();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        private void PrepareLanguageModel(LanguageModel model, Language entity)
        {
            if (null != entity)
            {
                model = entity.MapTo(model);
            }
        }
    }
}
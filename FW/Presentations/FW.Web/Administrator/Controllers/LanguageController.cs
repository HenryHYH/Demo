﻿namespace FW.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using FW.Admin.Models;
    using FW.Core.Domain.Localization;
    using FW.Service.Localization;
    using FW.Web.Framework.Extensions;

    public class LanguageController : Controller
    {
        #region Fields

        private readonly ILocalizationService localizationService;

        #endregion Fields

        #region Constructors

        public LanguageController(ILocalizationService localizationService)
        {
            this.localizationService = localizationService;
        }

        #endregion Constructors

        #region Methods

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

        public ActionResult Index()
        {
            return View();
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

        #endregion Methods
    }
}
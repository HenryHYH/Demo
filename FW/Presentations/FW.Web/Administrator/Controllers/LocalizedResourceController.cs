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
    using FW.Web.Framework.Controllers;

    public class LocalizedResourceController : Controller
    {
        #region Fields

        private readonly ILocalizationService localizationService;

        #endregion Fields

        #region Constructors

        public LocalizedResourceController(ILocalizationService localizationService)
        {
            this.localizationService = localizationService;
        }

        #endregion Constructors

        #region Methods

        public ActionResult Add()
        {
            var model = new LocalizedResourceModel();
            PrepareResourceModel(model, null);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName]
        public ActionResult Add(LocalizedResourceModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var entity = model.MapTo<LocalizedResourceModel, LocalizedResource>();
                localizationService.InsertResource(entity);

                if (continueEditing)
                    return RedirectToAction("Add");
                else
                    return RedirectToAction("Index", new { languageId = entity.LanguageId });
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = new LocalizedResourceModel();
            var entity = localizationService.GetResource(id);
            PrepareResourceModel(model, entity);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(LocalizedResourceModel model, bool continueEditing)
        {
            var entity = localizationService.GetResource(model.Id);
            if (null == entity)
                return RedirectToAction("Index", new { languageId = model.LanguageId });

            if (ModelState.IsValid)
            {
                entity = model.MapTo(entity);
                localizationService.UpdateResource(entity);

                if (continueEditing)
                    return RedirectToAction("Edit", new { id = entity.Id });
                else
                    return RedirectToAction("Index", new { languageId = entity.LanguageId });
            }

            return View(model);
        }

        public ActionResult Index(int languageId)
        {
            ViewBag.LanguageId = languageId;
            return View();
        }

        [HttpGet]
        public ActionResult ResourceList(int languageId, int pageIndex = 1, int pageSize = 20)
        {
            var list = localizationService.GetResources(languageId, pageIndex, pageSize)
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

        #endregion Methods
    }
}
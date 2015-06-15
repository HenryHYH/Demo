namespace FW.Admin.Controllers
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
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Add(LanguageModel model, bool continueEditing)
        {
            if (null == model)
                return Index();

            if (ModelState.IsValid)
            {
                var entity = model.MapTo<LanguageModel, Language>();
                localizationService.InsertLanguage(entity);

                if (continueEditing)
                    return RedirectToAction("Add");
                else
                    return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var entity = localizationService.GetLanguage(id);

            var model = new LanguageModel();
            PrepareLanguageModel(model, entity);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(LanguageModel model, bool continueEditing)
        {
            if (null == model)
                return Index();

            if (ModelState.IsValid)
            {
                var entity = model.MapTo<LanguageModel, Language>();
                localizationService.UpdateLanguage(entity);

                if (continueEditing)
                    return RedirectToAction("Edit", new { id = entity.Id });
                else
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
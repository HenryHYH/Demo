namespace FW.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using FluentValidation.Attributes;

    using FW.Admin.Validators;
    using FW.Core.Domain.Localization;
    using FW.Core.Infrastructure;
    using FW.Service.Localization;
    using FW.Web.Framework.Extensions;
    using FW.Web.Framework.MVC;
    using FW.Web.Framework.UI;

    [Validator(typeof(LocalizationValidator))]
    public class LocalizedResourceModel : BaseModel
    {
        #region Properties

        [ResourceDisplayName("Admin.LocalizedResource.Language")]
        public int LanguageId
        {
            get;
            set;
        }

        public string LanguageName { get; set; }

        [ResourceDisplayName("Admin.LocalizedResource.ResourceKey")]
        public string ResourceKey
        {
            get;
            set;
        }

        [ResourceDisplayName("Admin.LocalizedResource.ResourceValue")]
        public string ResourceValue
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        public IEnumerable<LanguageModel> GetLanguages()
        {
            var service = EngineContext.Current.Resolve<ILocalizationService>();

            return service.GetLanguages(pageSize: int.MaxValue)
                .ToModel<Language, LanguageModel>()
                .Data;
        }

        #endregion Methods
    }
}
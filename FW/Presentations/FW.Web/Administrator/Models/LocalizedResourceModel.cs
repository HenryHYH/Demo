using FluentValidation.Attributes;
using FW.Admin.Validators;
using FW.Core.Infrastructure;
using FW.Web.Framework.MVC;
using FW.Web.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FW.Admin.Models
{
    [Validator(typeof(LocalizationValidator))]
    public class LocalizedResourceModel : BaseModel
    {
        //[ResourceDisplayName("Admin.LocalizedResource.Language")]
        //public string Language { get; set; }

        [ResourceDisplayName("Admin.LocalizedResource.ResourceKey")]
        public string ResourceKey { get; set; }

        [ResourceDisplayName("Admin.LocalizedResource.ResourceValue")]
        public string ResourceValue { get; set; }

        //public IEnumerable<LanguageModel> GetLanguages()
        //{
        //    var service = EngineContext.Current.Resolve<ILanguageService>();
        //}
    }
}
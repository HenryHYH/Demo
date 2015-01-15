using FluentValidation.Attributes;
using FW.Admin.Validators;
using FW.Web.Framework.MVC;
using FW.Web.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FW.Admin.Models
{
    [Validator(typeof(LanguageValidator))]
    public class LanguageModel : BaseModel
    {
        [ResourceDisplayName("Admin.Language.Code")]
        public string Code { get; set; }

        [ResourceDisplayName("Admin.Language.Name")]
        public string Name { get; set; }
    }
}
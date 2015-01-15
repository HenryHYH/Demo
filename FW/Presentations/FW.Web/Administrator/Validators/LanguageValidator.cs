using FluentValidation;
using FW.Admin.Models;
using FW.Core.Infrastructure;
using FW.Service.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FW.Admin.Validators
{
    public class LanguageValidator : AbstractValidator<LanguageModel>
    {
        public LanguageValidator()
        {
            ILocalizationService localizationService = EngineContext.Current.Resolve<ILocalizationService>();

            RuleFor(x => x.Code).NotEmpty().WithLocalizedMessage(() => "Admin.Common.NotEmpty");
            RuleFor(x => x.Name).NotEmpty().WithLocalizedMessage(() => "Admin.Common.NotEmpty");
        }
    }
}
namespace FW.Admin.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using FluentValidation;

    using FW.Admin.Models;
    using FW.Core.Infrastructure;
    using FW.Service.Localization;

    public class LanguageValidator : AbstractValidator<LanguageModel>
    {
        #region Constructors

        public LanguageValidator()
        {
            ILocalizationService localizationService = EngineContext.Current.Resolve<ILocalizationService>();

            RuleFor(x => x.Code).NotEmpty().WithLocalizedMessage(() => "Admin.Common.NotEmpty");
            RuleFor(x => x.Name).NotEmpty().WithLocalizedMessage(() => "Admin.Common.NotEmpty");
        }

        #endregion Constructors
    }
}
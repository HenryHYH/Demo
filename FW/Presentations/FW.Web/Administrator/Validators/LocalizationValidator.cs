namespace FW.Admin.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using FluentValidation;

    using FW.Admin.Models;

    public class LocalizationValidator : AbstractValidator<LocalizedResourceModel>
    {
        #region Constructors

        public LocalizationValidator()
        {
            RuleFor(x => x.ResourceKey).NotEmpty().WithLocalizedMessage(() => "Admin.LocalizedResource.ResourceKey.NotEmpty");
            RuleFor(x => x.ResourceValue).NotEmpty().WithLocalizedMessage(() => "Admin.LocalizedResource.ResourceValue.NotEmpty");
        }

        #endregion Constructors
    }
}
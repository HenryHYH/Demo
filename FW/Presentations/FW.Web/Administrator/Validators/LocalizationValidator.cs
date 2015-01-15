using FluentValidation;
using FW.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FW.Admin.Validators
{
    public class LocalizationValidator : AbstractValidator<LocalizedResourceModel>
    {
        public LocalizationValidator()
        {
            RuleFor(x => x.ResourceKey).NotEmpty().WithLocalizedMessage(() => "Admin.LocalizedResource.ResourceKey.NotEmpty");
            RuleFor(x => x.ResourceValue).NotEmpty().WithLocalizedMessage(() => "Admin.LocalizedResource.ResourceValue.NotEmpty");
        }
    }
}
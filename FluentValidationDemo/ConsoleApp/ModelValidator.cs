using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    public class ModelValidator : BaseValidator<Model>
    {
        public ModelValidator()
        {
            // RuleFor(x => x.Name).NotEmpty().Length(1, 30);
            // RequiredLengthRuleFor(x => x.Name);
            NonRequiredLengthRuleFor(x => x.Name, 2);
            //NumberRuleFor(x => x.Age);
            //ArrayRuleFor(x => x.FavoriteSport, new[] { "Football" });
            //ArrayRuleFor(x => x.Age, new decimal[] { 1 });
        }
    }
}

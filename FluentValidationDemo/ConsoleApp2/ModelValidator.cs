using FluentValidation;

namespace ConsoleApp2
{
    public class ModelValidator : AbstractValidator<Model>
    {
        public ModelValidator()
        {
            RuleFor(x => x.Name).Length(2, 100);
        }
    }
}

using FluentValidation;
using Api.Resources;

namespace Api.Validators
{
    public class SaveModuleResourceValidator : AbstractValidator<ModuleResource>
    {
        public SaveModuleResourceValidator()
        {

            RuleFor(m => m.Id)
                .NotEmpty()
                .WithMessage("Id' must not be 0.");
        }
    }
}

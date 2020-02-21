using FluentValidation;
using Api.Resources;

namespace Api.Validators
{
    public class SaveActiveUserResourceValidator : AbstractValidator<ActiveUserResource>
    {
        public SaveActiveUserResourceValidator()
        {
         
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithMessage("Id' must not be 0.");
        }
    }
}

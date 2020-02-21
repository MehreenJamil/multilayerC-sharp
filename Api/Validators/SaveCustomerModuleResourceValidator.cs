using FluentValidation;
using Api.Resources;

namespace Api.Validators
{
    public class SaveCustomerModuleResourceValidator : AbstractValidator<CustomerModuleResource>
    {
        public SaveCustomerModuleResourceValidator()
        {

            RuleFor(m => m.Id)
                .NotEmpty()
                .WithMessage("Id' must not be 0.");
        }
    }
}

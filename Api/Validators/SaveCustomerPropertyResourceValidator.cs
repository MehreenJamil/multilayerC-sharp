using FluentValidation;
using Api.Resources;

namespace Api.Validators
{
    public class SaveCustomerPropertyResourceValidator : AbstractValidator<CustomerPropertyResource>
    {
        public SaveCustomerPropertyResourceValidator()
        {
            //RuleFor(m => m.Name)
            //    .NotEmpty()
            //    .MaximumLength(50);

            RuleFor(m => m.CustomerId)
                .NotEmpty()
                .WithMessage("'Customer Id' must not be 0.");
        }
    }
}

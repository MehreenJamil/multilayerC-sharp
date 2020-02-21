using FluentValidation;
using Api.Resources;


namespace Api.Validators
{
    public class SaveCustomerInspectionResourceValidator : AbstractValidator<CustomerInspectionResource>
    {
        public SaveCustomerInspectionResourceValidator()
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

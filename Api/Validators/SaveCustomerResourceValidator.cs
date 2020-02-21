using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Api.Resources;
namespace Api.Validators
{
    public class SaveCustomerResourceValidator : AbstractValidator<CustomerResource>
    {
        public SaveCustomerResourceValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(50).WithMessage("Name Must not be empty");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Api.Resources;

namespace Api.Validators
{
    public class SaveWorkerServiceLogResourceValidator : AbstractValidator<WorkerServiceLogResource>
    {

        public SaveWorkerServiceLogResourceValidator()
        {
            //RuleFor(m => m.Name)
            //    .NotEmpty()
            //    .MaximumLength(50);

            RuleFor(m => m.Start_time)
                .NotEmpty()
                .WithMessage("'Customer Id' must not be 0.");
        }
    }
}

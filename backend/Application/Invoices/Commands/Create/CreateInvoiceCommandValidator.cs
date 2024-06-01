using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Commands.Create
{
    public  class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator() {
            RuleFor(r => r.Title)
            .NotEmpty()
            .MaximumLength(200);

            RuleFor(r => r.Amount)
            .NotEmpty()
            .MaximumLength(200);

            RuleFor(r => r.Date)
            .Must(BeAValidDate)
            .WithMessage("Invalid date/time");
        }

        private bool BeAValidDate(string value)
        {
            DateTime date;
            return DateTime.TryParse(value, out date);
        }
    }
}

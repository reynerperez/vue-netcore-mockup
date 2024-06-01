using Application.Invoices.Commands.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Commands.Delete
{
    public class DeleteInvoiceCommandValidator: AbstractValidator<DeleteInvoiceCommand>
    {
        public DeleteInvoiceCommandValidator() {
            RuleFor(r => r.InvoiceId)
           .NotEmpty();
        }
    }
}

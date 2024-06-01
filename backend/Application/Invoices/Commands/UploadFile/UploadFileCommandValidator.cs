using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Commands.UploadFile
{
    public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
    {
        public UploadFileCommandValidator()
        {
            RuleFor(r => r.Id)
            .NotEmpty().NotNull();

            RuleFor(r => r.File)
            .NotEmpty()
            .SetValidator(new FileValidator());

        }
    }
}

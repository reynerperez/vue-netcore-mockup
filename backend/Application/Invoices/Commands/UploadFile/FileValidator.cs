using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Commands.UploadFile
{
    internal class FileValidator: AbstractValidator<IFormFile>
    {
   
            public FileValidator()
            {
                RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(512* 1024 * 1025)
                    .WithMessage("File size is larger than allowed");

                RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("application/pdf"))
                    .WithMessage("File type is not allowed");
        
        }
    }
}

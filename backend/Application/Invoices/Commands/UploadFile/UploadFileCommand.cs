using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Commands.UploadFile
{
    public class UploadFileCommand : IRequest<string>
    {
        public int Id { get; set; }
        public IFormFile File { get; set; } = default!;
    }
}

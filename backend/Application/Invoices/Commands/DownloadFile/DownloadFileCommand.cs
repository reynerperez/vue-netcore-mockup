using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Commands.DownlaodFile
{
    public class DownloadFileCommand : IRequest<byte[]?>
    {
        public int InvoiceId { get; set; }
    }
}

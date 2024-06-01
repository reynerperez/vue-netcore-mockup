using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Commands.Delete
{
    public class DeleteInvoiceCommand : IRequest
    {
        public int InvoiceId { get; set; }
    }
}

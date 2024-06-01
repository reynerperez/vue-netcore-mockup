using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Queries.Get
{
    public class GetInvoiceQuery : IRequest<InvoiceDTO>
    {
        public int InvoiceId { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Repository;
using System.Diagnostics;

namespace Application.Invoices.Queries.Get
{
    internal class GetInvoiceHandler : IRequestHandler<GetInvoiceQuery, InvoiceDTO?>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public GetInvoiceHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<InvoiceDTO?> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {

            Invoice invoice = await _invoiceRepository.GetInvoiceById(request.InvoiceId);

            return invoice == null ? new InvoiceDTO(invoice.InvoiceId, invoice.Name, invoice.Date.ToString("dd/M/yyyy"), invoice.Amount, invoice.File) : null;
        }
    }
}

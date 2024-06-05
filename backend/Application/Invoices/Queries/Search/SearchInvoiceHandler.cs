using Domain.Entities;
using Infrastructure.Repository;
using MediatR;

namespace Application.Invoices.Queries.Search
{
    internal class SearchInvoiceHandler : IRequestHandler<SearchInvoiceQuery, List<InvoiceDTO>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public SearchInvoiceHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public async Task<List<InvoiceDTO>> Handle(SearchInvoiceQuery request, CancellationToken cancellationToken)
        {


            List<InvoiceDTO> invoiceDTOs = new List<InvoiceDTO>();
            List<Invoice> invoices = await _invoiceRepository.SearchInvoices(request.Term!);

            foreach (Invoice invoice in invoices)
            {
                invoiceDTOs.Add(new InvoiceDTO(invoice.InvoiceId, invoice.Name, invoice.Date.ToString("dd/MM/yyyy"), invoice.Amount, invoice.File));
            }

            return invoiceDTOs;
        }
    }
}
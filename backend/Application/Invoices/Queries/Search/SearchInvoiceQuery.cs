

using MediatR;

namespace Application.Invoices.Queries.Search
{
    public class SearchInvoiceQuery : IRequest<List<InvoiceDTO>>
    {
        public string? Term { get; set; } = string.Empty;
    }
}
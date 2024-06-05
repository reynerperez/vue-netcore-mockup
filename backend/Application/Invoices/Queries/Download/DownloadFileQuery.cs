using MediatR;

namespace Application.Invoices.Queries.Downlaod
{
    public class DownloadFileQuery : IRequest<byte[]?>
    {
        public int InvoiceId { get; set; }
    }
}

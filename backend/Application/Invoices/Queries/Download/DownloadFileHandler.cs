using Domain.Entities;
using Infrastructure.Repository;
using MediatR;

namespace Application.Invoices.Queries.Downlaod
{
    public class DownloadFileHandler : IRequestHandler<DownloadFileQuery, byte[]?>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public DownloadFileHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public async Task<byte[]?> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {

            Invoice? invoice = await _invoiceRepository.GetInvoiceById(request.InvoiceId);
            if (invoice != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Storage");
                string fullPath = Path.Combine(path, invoice.File);
                if (File.Exists(fullPath))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
                    return fileBytes;
                }

            }
            return null;
        }
    }
}

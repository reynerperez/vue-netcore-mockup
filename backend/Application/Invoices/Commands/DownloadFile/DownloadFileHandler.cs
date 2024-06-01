using Application.Invoices.Commands.UploadFile;
using Domain.Entities;
using Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Commands.DownlaodFile
{
    public class DownloadFileHandler : IRequestHandler<DownloadFileCommand, byte[]?>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public DownloadFileHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public async Task<byte[]?> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
        {

            Invoice invoice = await _invoiceRepository.GetInvoiceById(request.InvoiceId);
            if (invoice != null) { 
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

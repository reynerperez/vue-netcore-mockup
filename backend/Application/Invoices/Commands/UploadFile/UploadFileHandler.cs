using Application.Invoices.Commands.Create;
using Domain.Entities;
using Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Commands.UploadFile
{
    internal class UploadFileHandler : IRequestHandler<UploadFileCommand, string>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public UploadFileHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            Invoice? invoice = await _invoiceRepository.GetInvoiceById(request.Id);
            if (invoice != null)
            {

                string extention = Path.GetExtension(request.File.FileName);
                string filename = Guid.NewGuid().ToString() + extention;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Storage");

                using (FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    request.File.CopyTo(stream);

                invoice.File = filename;
                await _invoiceRepository.UpdateInvoice(invoice);

                return filename;
            }

            return string.Empty;
        }
    }
}

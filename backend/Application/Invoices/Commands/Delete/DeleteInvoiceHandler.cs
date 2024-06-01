using Application.Invoices.Commands.Create;
using Domain.Entities;
using Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Commands.Delete
{
    internal class DeleteInvoiceHandler: IRequestHandler<DeleteInvoiceCommand>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public DeleteInvoiceHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public async Task Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            await _invoiceRepository.Delete(request.InvoiceId);
        }
    }
}

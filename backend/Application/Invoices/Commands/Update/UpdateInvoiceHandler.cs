using Application.Invoices.Commands.Create;
using Domain.Entities;
using Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Commands.Update
{
    internal class UpdateInvoiceHandler : IRequestHandler<UpdateInvoiceCommand>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public UpdateInvoiceHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            Invoice toUpdate = new Invoice()
            {
                InvoiceId = request.Id,
                Name = request.Title,
                Date = DateTime.Parse(request.Date),
                Amount = request.Amount,
                File = request.File,
            };

            await _invoiceRepository.UpdateInvoice(toUpdate);
        }
    }
}

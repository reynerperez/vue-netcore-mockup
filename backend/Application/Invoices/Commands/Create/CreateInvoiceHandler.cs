﻿using Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Invoices.Commands.Create
{
    internal class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, int?>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public CreateInvoiceHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public async Task<int?> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            Invoice toCreate = new Invoice()
            {
                Name = request.Title,
                Date = DateTime.Parse(request.Date),
                Amount = request.Amount,
                File = request.File,
            };

            int id = await _invoiceRepository.AddInvoice(toCreate);
            return id;
        }
    }
}

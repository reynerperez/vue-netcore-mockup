using Application.Invoices.Queries.Get;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Repository;
using AutoMapper;

namespace Application.Invoices.Queries.List
{
    public class ListInvoiceHandler : IRequestHandler<ListInvoiceQuery, List<InvoiceDTO>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public ListInvoiceHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<List<InvoiceDTO>> Handle(ListInvoiceQuery request, CancellationToken cancellationToken)
        {

            List<InvoiceDTO> invoiceDTOs = new List<InvoiceDTO>();
            List<Invoice> invoices = await _invoiceRepository.GetAll();

            foreach (Invoice invoice in invoices)
            {
                invoiceDTOs.Add(new InvoiceDTO(invoice.InvoiceId, invoice.Name, invoice.Date.ToString("dd/MM/yyyy"), invoice.Amount, invoice.File));
            }

            return invoiceDTOs;

        }
    }
}

using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Commands.Create
{
    public class CreateInvoiceCommand : IRequest<int?>
    {
        public string Title { get; set; } = default!;
        public string Date { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;
        public string File { get; set; } = string.Empty;
    }
}

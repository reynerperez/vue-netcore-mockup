using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Commands.Update
{
    public class UpdateInvoiceCommand : IRequest
    {
        public int Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;
        public string File { get; set; } = string.Empty;
    }
}

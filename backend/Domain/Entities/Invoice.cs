using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Invoice
    {
        public int InvoiceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Amount { get; set; } = string.Empty;
        public string File { get; set; } = string.Empty;
    }
}

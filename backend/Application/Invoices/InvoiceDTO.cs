using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices
{
    public record InvoiceDTO(int Id, string Title, string Date, string amount, string file);
}

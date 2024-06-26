﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
        public interface IInvoiceRepository
        {

                Task<List<Invoice>> SearchInvoices(string term);

                Task<List<Invoice>> GetAll();

                Task<Invoice?> GetInvoiceById(int invoiceId);

                Task<int> AddInvoice(Invoice toCreate);

                Task UpdateInvoice(Invoice toUpdate);

                Task Delete(int invoiceId);
        }
}

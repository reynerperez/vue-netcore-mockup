using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Infrastructure.Repositories
{
    public abstract class BasePostgresSqlRepository
    {

        public async Task<NpgsqlConnection> getConnection()
        {
            string connString = "Host=localhost;Username=postgres;Password=admin;Database=mockup";

            NpgsqlDataSourceBuilder dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);
            NpgsqlDataSource dataSource = dataSourceBuilder.Build();

            NpgsqlConnection conn = await dataSource.OpenConnectionAsync();
            return conn;
        }


        public static class StoredProcedures
        {
            public static string GetAllInvoices = $"select * from get_all_invoices()";
            public static string GetInvoicesById = $"select * from get_invoice_by_id(@id)";
            public static string CreateInvoice = $"call create_invoice(@name, @date, @amount, @file)";
            public static string UpdateInvoice = $"call update_invoice(@id, @name, @date, @amount, @file)";
            public static string DeleteInvoice = $"call delete_invoice(@id)";


        }
    }
}

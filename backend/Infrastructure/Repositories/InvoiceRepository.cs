using Domain.Entities;
using Infrastructure.Repositories;
using Npgsql;
using Serilog;
using System.Data;
using System.Text;


namespace Infrastructure.Repository
{
    public class InvoiceRepository : BasePostgresSqlRepository, IInvoiceRepository
    {
        public async Task<List<Invoice>> GetAll()
        {
            try
            {
                var list = new List<Invoice>();
                await using (var conn = await getConnection()) 
                await using (var cmd = new NpgsqlCommand(StoredProcedures.GetAllInvoices, conn))
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Invoice invoice = new Invoice();
                        invoice.InvoiceId = reader.GetInt32("id");
                        invoice.Name = reader.GetString("name");
                        invoice.Date = reader.GetDateTime("date");
                        invoice.Amount = reader.GetString("amount");
                        invoice.File = reader.IsDBNull("file") ? "" : reader.GetString("file");

                        list.Add(invoice);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                Log.Error("GetAll Invoice: {@Exception}", ex);
                throw;
            }
        }

        public async Task<Invoice?> GetInvoiceById(int invoiceId)
        {
            try
            {
                await using (var conn = await getConnection())
                await using (var cmd = new NpgsqlCommand(StoredProcedures.GetInvoicesById, conn))
                {
                    cmd.Parameters.AddWithValue("id", invoiceId);
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Invoice invoice = new Invoice();
                            invoice.InvoiceId = reader.GetInt32("id");
                            invoice.Name = reader.GetString("name");
                            invoice.Date = reader.GetDateTime("date");
                            invoice.Amount = reader.GetString("amount");
                            invoice.File = reader.IsDBNull("file") ? "" : reader.GetString("file");
                            return invoice;
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Get Invoice By ID: {@Exception}", ex);
                throw;
            }
        }

        public async Task AddInvoice(Invoice toCreate)
        {
            try
            {
                await using (var conn = await getConnection())
                await using (var cmd = new NpgsqlCommand(StoredProcedures.CreateInvoice, conn))
                {
                    cmd.Parameters.AddWithValue("name", toCreate.Name);
                    cmd.Parameters.AddWithValue("date", toCreate.Date);
                    cmd.Parameters.AddWithValue("amount", toCreate.Amount);
                    cmd.Parameters.AddWithValue("file", toCreate.File);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Add Invoice: {@Exception}", ex);
                throw;
            }

        }

        public async Task UpdateInvoice(Invoice toUpdate)
        {
            try
            {
                await using (var conn = await getConnection())
                await using (var cmd = new NpgsqlCommand(StoredProcedures.UpdateInvoice, conn))
                {
                    cmd.Parameters.AddWithValue("id", toUpdate.InvoiceId);
                    cmd.Parameters.AddWithValue("name", toUpdate.Name);
                    cmd.Parameters.AddWithValue("date", toUpdate.Date);
                    cmd.Parameters.AddWithValue("amount", toUpdate.Amount);
                    cmd.Parameters.AddWithValue("file", toUpdate.File);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Update Invoice: {@Exception}", ex);
                throw;
            }
        }

        public async Task Delete(int invoiceId)
        {
            try
            {
                await using (var conn = await getConnection())
                await using (var cmd = new NpgsqlCommand(StoredProcedures.DeleteInvoice, conn))
                {
                    cmd.Parameters.AddWithValue("id", invoiceId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Delete Invoice: {@Exception}", ex);
                throw;
            }
        }
    }
}

using InvoiceManager.Invoices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace InvoiceManager.Repositories;

public interface IInvoiceRepository : IRepository<Invoice, Guid>
{
    Task<Invoice?> GetByIdIncludeDetailsAsync(Guid id);
    Task<List<Invoice>> GetFilteredListAsync(int skipCount, int maxResultCount, string invoiceNumberFilter = null, InvoiceState? state = null);
}

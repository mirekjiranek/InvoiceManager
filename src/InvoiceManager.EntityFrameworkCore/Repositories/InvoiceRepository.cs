using InvoiceManager.EntityFrameworkCore;
using InvoiceManager.Invoices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace InvoiceManager.Repositories;

public class InvoiceRepository :
    EfCoreRepository<InvoiceManagerDbContext, Invoice, Guid>,
    IInvoiceRepository
{
    public InvoiceRepository(IDbContextProvider<InvoiceManagerDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Invoice> GetByIdIncludeDetailsAsync(Guid id)
    {
        return await GetAsync(id, includeDetails: true);
    }

    public async Task<List<Invoice>> GetFilteredListAsync(
        int skipCount,
        int maxResultCount,
        string invoiceNumberFilter = null,
        InvoiceState? state = null)
    {
        var queryable = (await GetQueryableAsync())
            .Include(x => x.Lines)
            .WhereIf(!invoiceNumberFilter.IsNullOrEmpty(),
                x => x.InvoiceNumber.Contains(invoiceNumberFilter))
            .WhereIf(state.HasValue,
                x => x.State == state.Value);

        var items = await queryable
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();

        return items;
    }
}

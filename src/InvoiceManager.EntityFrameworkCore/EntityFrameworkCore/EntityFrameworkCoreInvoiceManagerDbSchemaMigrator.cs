using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InvoiceManager.Data;
using Volo.Abp.DependencyInjection;

namespace InvoiceManager.EntityFrameworkCore;

public class EntityFrameworkCoreInvoiceManagerDbSchemaMigrator
    : IInvoiceManagerDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreInvoiceManagerDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the InvoiceManagerDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<InvoiceManagerDbContext>()
            .Database
            .MigrateAsync();
    }
}

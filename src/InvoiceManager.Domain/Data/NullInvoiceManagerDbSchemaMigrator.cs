using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace InvoiceManager.Data;

/* This is used if database provider does't define
 * IInvoiceManagerDbSchemaMigrator implementation.
 */
public class NullInvoiceManagerDbSchemaMigrator : IInvoiceManagerDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

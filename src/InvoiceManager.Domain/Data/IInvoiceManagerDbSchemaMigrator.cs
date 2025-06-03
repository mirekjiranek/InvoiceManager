using System.Threading.Tasks;

namespace InvoiceManager.Data;

public interface IInvoiceManagerDbSchemaMigrator
{
    Task MigrateAsync();
}

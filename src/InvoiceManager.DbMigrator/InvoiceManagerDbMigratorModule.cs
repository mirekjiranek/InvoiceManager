using InvoiceManager.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace InvoiceManager.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(InvoiceManagerEntityFrameworkCoreModule),
    typeof(InvoiceManagerApplicationContractsModule)
)]
public class InvoiceManagerDbMigratorModule : AbpModule
{
}

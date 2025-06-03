using Volo.Abp.Modularity;

namespace InvoiceManager;

[DependsOn(
    typeof(InvoiceManagerDomainModule),
    typeof(InvoiceManagerTestBaseModule)
)]
public class InvoiceManagerDomainTestModule : AbpModule
{

}

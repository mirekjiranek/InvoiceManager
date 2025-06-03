using Volo.Abp.Modularity;

namespace InvoiceManager;

[DependsOn(
    typeof(InvoiceManagerApplicationModule),
    typeof(InvoiceManagerDomainTestModule)
)]
public class InvoiceManagerApplicationTestModule : AbpModule
{

}

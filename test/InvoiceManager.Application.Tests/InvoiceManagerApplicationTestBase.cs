using Volo.Abp.Modularity;

namespace InvoiceManager;

public abstract class InvoiceManagerApplicationTestBase<TStartupModule> : InvoiceManagerTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

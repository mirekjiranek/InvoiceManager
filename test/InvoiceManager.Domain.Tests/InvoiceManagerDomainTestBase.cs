using Volo.Abp.Modularity;

namespace InvoiceManager;

/* Inherit from this class for your domain layer tests. */
public abstract class InvoiceManagerDomainTestBase<TStartupModule> : InvoiceManagerTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

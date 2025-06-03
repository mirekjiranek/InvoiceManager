using InvoiceManager.Localization;
using Volo.Abp.Application.Services;

namespace InvoiceManager;

/* Inherit your application services from this class.
 */
public abstract class InvoiceManagerAppService : ApplicationService
{
    protected InvoiceManagerAppService()
    {
        LocalizationResource = typeof(InvoiceManagerResource);
    }
}

using InvoiceManager.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace InvoiceManager.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class InvoiceManagerController : AbpControllerBase
{
    protected InvoiceManagerController()
    {
        LocalizationResource = typeof(InvoiceManagerResource);
    }
}

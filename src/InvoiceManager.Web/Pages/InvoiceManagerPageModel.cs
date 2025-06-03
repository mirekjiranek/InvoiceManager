using InvoiceManager.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace InvoiceManager.Web.Pages;

public abstract class InvoiceManagerPageModel : AbpPageModel
{
    protected InvoiceManagerPageModel()
    {
        LocalizationResourceType = typeof(InvoiceManagerResource);
    }
}

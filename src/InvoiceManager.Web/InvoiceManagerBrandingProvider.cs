using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using InvoiceManager.Localization;

namespace InvoiceManager.Web;

[Dependency(ReplaceServices = true)]
public class InvoiceManagerBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<InvoiceManagerResource> _localizer;

    public InvoiceManagerBrandingProvider(IStringLocalizer<InvoiceManagerResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}

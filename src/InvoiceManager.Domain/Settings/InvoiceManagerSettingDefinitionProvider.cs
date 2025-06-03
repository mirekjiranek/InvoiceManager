using Volo.Abp.Settings;

namespace InvoiceManager.Settings;

public class InvoiceManagerSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(InvoiceManagerSettings.MySetting1));
    }
}

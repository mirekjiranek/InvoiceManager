using InvoiceManager.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace InvoiceManager.Permissions;

public class InvoiceManagerPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(InvoiceManagerPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(InvoiceManagerPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InvoiceManagerResource>(name);
    }
}

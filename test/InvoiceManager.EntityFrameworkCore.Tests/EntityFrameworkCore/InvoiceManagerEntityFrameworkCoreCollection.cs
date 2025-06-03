using Xunit;

namespace InvoiceManager.EntityFrameworkCore;

[CollectionDefinition(InvoiceManagerTestConsts.CollectionDefinitionName)]
public class InvoiceManagerEntityFrameworkCoreCollection : ICollectionFixture<InvoiceManagerEntityFrameworkCoreFixture>
{

}

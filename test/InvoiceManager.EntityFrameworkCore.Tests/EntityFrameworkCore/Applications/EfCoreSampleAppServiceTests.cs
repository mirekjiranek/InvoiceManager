using InvoiceManager.Samples;
using Xunit;

namespace InvoiceManager.EntityFrameworkCore.Applications;

[Collection(InvoiceManagerTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<InvoiceManagerEntityFrameworkCoreTestModule>
{

}

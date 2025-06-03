using InvoiceManager.Samples;
using Xunit;

namespace InvoiceManager.EntityFrameworkCore.Domains;

[Collection(InvoiceManagerTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<InvoiceManagerEntityFrameworkCoreTestModule>
{

}

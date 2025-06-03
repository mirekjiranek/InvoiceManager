using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace InvoiceManager.Pages;

[Collection(InvoiceManagerTestConsts.CollectionDefinitionName)]
public class Index_Tests : InvoiceManagerWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}

using Microsoft.AspNetCore.Builder;
using InvoiceManager;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("InvoiceManager.Web.csproj"); 
await builder.RunAbpModuleAsync<InvoiceManagerWebTestModule>(applicationName: "InvoiceManager.Web");

public partial class Program
{
}

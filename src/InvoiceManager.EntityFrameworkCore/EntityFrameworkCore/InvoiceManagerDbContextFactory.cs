using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InvoiceManager.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class InvoiceManagerDbContextFactory : IDesignTimeDbContextFactory<InvoiceManagerDbContext>
{
    public InvoiceManagerDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        InvoiceManagerEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<InvoiceManagerDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new InvoiceManagerDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../InvoiceManager.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}

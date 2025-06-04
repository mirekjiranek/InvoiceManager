using InvoiceManager.Invoices;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace InvoiceManager.Extensions;

public static class DbContextModelCreatingExtensions
{
    public static void ConfigureInvoiceManager(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Invoice>(b =>
        {
            b.ToTable("Invoices");
            b.ConfigureByConvention(); 

            b.Property(x => x.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(InvoiceConsts.MaxInvoiceNumberLength);

            b.Property(x => x.IssueDate).IsRequired();
            b.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");
            b.Property(x => x.State).IsRequired();

            b.HasMany(x => x.Lines)
                .WithOne()
                .HasForeignKey(x => x.InvoiceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            b.HasIndex(x => x.InvoiceNumber).IsUnique();
        });

        builder.Entity<InvoiceLine>(b =>
        {
            b.ToTable("InvoiceLines");
            b.ConfigureByConvention();

            b.Property(x => x.InvoiceId).IsRequired();
            b.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(InvoiceConsts.MaxProductNameLength);
            b.Property(x => x.Quantity).IsRequired();
            b.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
            b.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");
        });
    }
}

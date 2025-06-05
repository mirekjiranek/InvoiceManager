using System;
using Xunit;

namespace InvoiceManager.Invoices;

public class InvoiceLineTests
{
    private readonly Guid TestInvoiceId = Guid.NewGuid();
    private const string TestProductName = "Test Product";

    [Fact]
    public void Should_Create_InvoiceLine_With_Valid_Data()
    {
        // Act
        var line = new InvoiceLine(Guid.NewGuid(), TestInvoiceId, TestProductName, 5, 10.50m);

        // Assert
        Assert.NotNull(line);
        Assert.Equal(TestInvoiceId, line.InvoiceId);
        Assert.Equal(TestProductName, line.ProductName);
        Assert.Equal(5, line.Quantity);
        Assert.Equal(10.50m, line.UnitPrice);
        Assert.Equal(52.50m, line.TotalPrice);
    }

    [Fact]
    public void Should_Not_Create_InvoiceLine_With_Invalid_Quantity()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new InvoiceLine(Guid.NewGuid(), TestInvoiceId, TestProductName, 0, 10m));

        Assert.Throws<ArgumentException>(() =>
            new InvoiceLine(Guid.NewGuid(), TestInvoiceId, TestProductName, -1, 10m));
    }

    [Fact]
    public void Should_Not_Create_InvoiceLine_With_Invalid_Price()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new InvoiceLine(Guid.NewGuid(), TestInvoiceId, TestProductName, 1, 0m));

        Assert.Throws<ArgumentException>(() =>
            new InvoiceLine(Guid.NewGuid(), TestInvoiceId, TestProductName, 1, -10m));
    }

    [Fact]
    public void Should_Not_Create_InvoiceLine_With_Empty_ProductName()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new InvoiceLine(Guid.NewGuid(), TestInvoiceId, "", 1, 10m));

        Assert.Throws<ArgumentException>(() =>
            new InvoiceLine(Guid.NewGuid(), TestInvoiceId, null, 1, 10m));
    }

    [Fact]
    public void Should_Update_InvoiceLine()
    {
        // Arrange
        var line = new InvoiceLine(Guid.NewGuid(), TestInvoiceId, TestProductName, 5, 10m);

        // Act
        line.Update(10, 15m);

        // Assert
        Assert.Equal(10, line.Quantity);
        Assert.Equal(15m, line.UnitPrice);
        Assert.Equal(150m, line.TotalPrice);
    }

    [Fact]
    public void Should_Calculate_TotalPrice_Correctly()
    {
        // Arrange & Act
        var line1 = new InvoiceLine(Guid.NewGuid(), TestInvoiceId, "Product 1", 3, 7.50m);
        var line2 = new InvoiceLine(Guid.NewGuid(), TestInvoiceId, "Product 2", 10, 2.25m);

        // Assert
        Assert.Equal(22.50m, line1.TotalPrice);
        Assert.Equal(22.50m, line2.TotalPrice);
    }
}

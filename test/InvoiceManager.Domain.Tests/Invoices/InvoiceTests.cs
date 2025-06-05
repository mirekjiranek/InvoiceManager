using System;
using Volo.Abp;
using Xunit;

namespace InvoiceManager.Invoices;

public class InvoiceTests
{
    private const string TestInvoiceNumber = "TEST-001";
    private readonly DateTime TestIssueDate = new DateTime(2024, 1, 15);

    [Fact]
    public void Should_Create_Invoice_With_Valid_Data()
    {
        // Act
        var invoice = new Invoice(Guid.NewGuid(), TestInvoiceNumber, TestIssueDate);

        // Assert
        Assert.NotNull(invoice);
        Assert.Equal(TestInvoiceNumber, invoice.InvoiceNumber);
        Assert.Equal(TestIssueDate, invoice.IssueDate);
        Assert.Equal(0, invoice.TotalAmount);
        Assert.Equal(InvoiceState.Created, invoice.State);
        Assert.Empty(invoice.Lines);
    }

    [Fact]
    public void Should_Not_Create_Invoice_With_Empty_InvoiceNumber()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Invoice(Guid.NewGuid(), "", TestIssueDate));

        Assert.Throws<ArgumentException>(() =>
            new Invoice(Guid.NewGuid(), null, TestIssueDate));
    }

    [Fact]
    public void Should_Add_Line_To_Invoice()
    {
        // Arrange
        var invoice = new Invoice(Guid.NewGuid(), TestInvoiceNumber, TestIssueDate);
        const string productName = "Test Product";
        const int quantity = 5;
        const decimal unitPrice = 10.50m;

        // Act
        var line = invoice.AddLine(productName, quantity, unitPrice);

        // Assert
        Assert.NotNull(line);
        Assert.Single(invoice.Lines);
        Assert.Equal(productName, line.ProductName);
        Assert.Equal(quantity, line.Quantity);
        Assert.Equal(unitPrice, line.UnitPrice);
        Assert.Equal(52.50m, line.TotalPrice);
        Assert.Equal(52.50m, invoice.TotalAmount);
    }

    [Fact]
    public void Should_Calculate_TotalAmount_When_Adding_Multiple_Lines()
    {
        // Arrange
        var invoice = new Invoice(Guid.NewGuid(), TestInvoiceNumber, TestIssueDate);

        // Act
        invoice.AddLine("Product 1", 2, 10m);
        invoice.AddLine("Product 2", 3, 15m);
        invoice.AddLine("Product 3", 1, 25m);

        // Assert
        Assert.Equal(3, invoice.Lines.Count);
        Assert.Equal(90m, invoice.TotalAmount); 
    }

    [Fact]
    public void Should_Remove_Line_From_Invoice()
    {
        // Arrange
        var invoice = new Invoice(Guid.NewGuid(), TestInvoiceNumber, TestIssueDate);
        var line1 = invoice.AddLine("Product 1", 2, 10m);
        var line2 = invoice.AddLine("Product 2", 3, 15m);

        // Act
        invoice.RemoveLine(line1.Id);

        // Assert
        Assert.Single(invoice.Lines);
        Assert.Equal(45m, invoice.TotalAmount);
        Assert.DoesNotContain(invoice.Lines, l => l.Id == line1.Id);
    }

    [Fact]
    public void Should_Update_Line_In_Invoice()
    {
        // Arrange
        var invoice = new Invoice(Guid.NewGuid(), TestInvoiceNumber, TestIssueDate);
        var line = invoice.AddLine("Product", 2, 10m);

        // Act
        invoice.UpdateLine(line.Id, 5, 12m);

        // Assert
        Assert.Equal(5, line.Quantity);
        Assert.Equal(12m, line.UnitPrice);
        Assert.Equal(60m, line.TotalPrice);
        Assert.Equal(60m, invoice.TotalAmount);
    }

    [Fact]
    public void Should_Throw_When_Updating_NonExistent_Line()
    {
        // Arrange
        var invoice = new Invoice(Guid.NewGuid(), TestInvoiceNumber, TestIssueDate);

        // Act & Assert
        var exception = Assert.Throws<BusinessException>(() =>
            invoice.UpdateLine(Guid.NewGuid(), 1, 10m));
    }

    [Fact]
    public void Should_Approve_Invoice_With_Lines()
    {
        // Arrange
        var invoice = new Invoice(Guid.NewGuid(), TestInvoiceNumber, TestIssueDate);
        invoice.AddLine("Product", 1, 10m);

        // Act
        invoice.Approve();

        // Assert
        Assert.Equal(InvoiceState.Approved, invoice.State);
    }

    [Fact]
    public void Should_Not_Approve_Empty_Invoice()
    {
        // Arrange
        var invoice = new Invoice(Guid.NewGuid(), TestInvoiceNumber, TestIssueDate);

        // Act & Assert
        var exception = Assert.Throws<BusinessException>(() => invoice.Approve());
    }

    [Fact]
    public void Should_Not_Approve_Already_Paid_Invoice()
    {
        // Arrange
        var invoice = new Invoice(Guid.NewGuid(), TestInvoiceNumber, TestIssueDate);
        invoice.AddLine("Product", 1, 10m);
        invoice.Approve();
        invoice.Pay();

        // Act & Assert
        var exception = Assert.Throws<BusinessException>(() => invoice.Approve());
    }

    [Fact]
    public void Should_Pay_Approved_Invoice()
    {
        // Arrange
        var invoice = new Invoice(Guid.NewGuid(), TestInvoiceNumber, TestIssueDate);
        invoice.AddLine("Product", 1, 10m);
        invoice.Approve();

        // Act
        invoice.Pay();

        // Assert
        Assert.Equal(InvoiceState.Paid, invoice.State);
    }

    [Fact]
    public void Should_Not_Pay_Unapproved_Invoice()
    {
        // Arrange
        var invoice = new Invoice(Guid.NewGuid(), TestInvoiceNumber, TestIssueDate);
        invoice.AddLine("Product", 1, 10m);

        // Act & Assert
        var exception = Assert.Throws<BusinessException>(() => invoice.Pay());
    }
}

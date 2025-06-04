using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace InvoiceManager.Invoices;

public class InvoiceLine : Entity<Guid>
{
    public virtual Guid InvoiceId { get; private set; }
    public virtual Guid ProductId { get; private set; }
    public virtual string ProductName { get; private set; }
    public virtual int Quantity { get; private set; }
    public virtual decimal UnitPrice { get; private set; }
    public virtual decimal TotalPrice { get; private set; }

    protected InvoiceLine() { }

    internal InvoiceLine(Guid id, Guid invoiceId, Guid productId, string productName, int quantity, decimal unitPrice) : base(id)
    {
        Check.Positive(quantity, nameof(quantity));
        Check.Positive(unitPrice, nameof(unitPrice));

        InvoiceId = invoiceId;
        ProductId = productId;
        SetProductName(productName);
        Update(quantity, unitPrice);
    }

    internal virtual void Update(int quantity, decimal unitPrice)
    {
        Check.Positive(quantity, nameof(quantity));
        Check.Positive(unitPrice, nameof(unitPrice));

        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalPrice = quantity * unitPrice;
    }

    private void SetProductName(string productName)
    {
        ProductName = Check.NotNullOrWhiteSpace(
            productName,
            nameof(productName),
            InvoiceConsts.MaxProductNameLength
        );
    }
}

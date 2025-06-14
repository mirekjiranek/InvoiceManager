﻿using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace InvoiceManager.Invoices;

public class InvoiceLine : Entity<Guid>
{
    public virtual Guid InvoiceId { get; private set; }
    public virtual string ProductName { get; private set; } = string.Empty;
    public virtual int Quantity { get; private set; }
    public virtual decimal UnitPrice { get; private set; }
    public virtual decimal TotalPrice { get; private set; }

    protected InvoiceLine() 
    {
        ProductName = string.Empty;
    }

    internal InvoiceLine(Guid id, Guid invoiceId, string productName, int quantity, decimal unitPrice) : base(id)
    {
        Check.Positive(quantity, nameof(quantity));
        Check.Positive(unitPrice, nameof(unitPrice));

        InvoiceId = invoiceId;
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

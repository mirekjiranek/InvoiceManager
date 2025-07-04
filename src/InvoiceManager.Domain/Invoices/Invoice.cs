﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace InvoiceManager.Invoices;

public class Invoice : FullAuditedAggregateRoot<Guid>
{
    public virtual string InvoiceNumber { get; private set; } = string.Empty;
    public virtual DateTime IssueDate { get; private set; }
    public virtual decimal TotalAmount { get; private set; }
    public virtual InvoiceState State { get; private set; }

    public virtual ICollection<InvoiceLine> Lines { get; protected set; }

    protected Invoice() 
    {
        InvoiceNumber = string.Empty;
        Lines = new Collection<InvoiceLine>();
    }

    public Invoice(Guid id, string invoiceNumber, DateTime issueDate) : base(id)
    {
        SetInvoiceNumber(invoiceNumber);
        IssueDate = issueDate;
        State = InvoiceState.Created;
        Lines = new Collection<InvoiceLine>();
        TotalAmount = 0;
    }

    public virtual InvoiceLine AddLine(string productName, int quantity, decimal unitPrice)
    {
        var line = new InvoiceLine(Guid.NewGuid(), Id, productName, quantity, unitPrice);
        Lines.Add(line);
        RecalculateTotalAmount();
        return line;
    }

    public virtual void RemoveLine(Guid productId)
    {
        var line = Lines.SingleOrDefault(l => l.Id == productId);
        if (line != null)
        {
            Lines.Remove(line);
            RecalculateTotalAmount();
        }
    }

    public virtual void UpdateLine(Guid productId, int quantity, decimal unitPrice)
    {
        var line = Lines.SingleOrDefault(l => l.Id == productId) ?? throw new BusinessException("InvoiceLine:NotFound", $"Invoice line with id {productId} not found");
        line.Update(quantity, unitPrice);
        RecalculateTotalAmount();
    }

    public virtual void Approve()
    {
        if (State == InvoiceState.Paid)
        {
            throw new BusinessException(null, $"Invoice is already in {InvoiceState.Paid} state and cannot be set to {InvoiceState.Approved} state");
        }

        if (TotalAmount == 0)
        {
            throw new BusinessException(null, "Invoice must have at least one line");
        }      
        
        State = InvoiceState.Approved;
    }

    public virtual void Pay()
    {
        if (State != InvoiceState.Approved)
        {
            throw new BusinessException(null, $"Invoice can only be paid when in {InvoiceState.Approved} state. Current state: {State}");
        }
        State = InvoiceState.Paid;
    }

    private void SetInvoiceNumber(string invoiceNumber)
    {
        InvoiceNumber = Check.NotNullOrWhiteSpace(
            invoiceNumber,
            nameof(invoiceNumber),
            InvoiceConsts.MaxInvoiceNumberLength
        );
    }

    private void RecalculateTotalAmount()
    {
        TotalAmount = Lines.Sum(l => l.TotalPrice);
    }
}

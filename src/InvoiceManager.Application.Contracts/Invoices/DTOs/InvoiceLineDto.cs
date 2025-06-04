using System;
using Volo.Abp.Application.Dtos;

namespace InvoiceManager.Invoices.DTOs;

public class InvoiceLineDto : EntityDto<Guid>
{
    public Guid InvoiceId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}

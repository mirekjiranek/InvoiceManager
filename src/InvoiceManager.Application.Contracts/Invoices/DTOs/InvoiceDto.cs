using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace InvoiceManager.Invoices.DTOs;

public class InvoiceDto : FullAuditedEntityDto<Guid>
{
    public string InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public InvoiceState State { get; set; }
    public List<InvoiceLineDto> Lines { get; set; } = new List<InvoiceLineDto>();
}

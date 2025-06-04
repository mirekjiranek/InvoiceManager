using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.Invoices.DTOs;

public class CreateInvoiceDto
{
    [Required]
    [StringLength(InvoiceConsts.MaxInvoiceNumberLength, MinimumLength = 1)]
    public string InvoiceNumber { get; set; }

    [Required]
    public DateTime IssueDate { get; set; }
}

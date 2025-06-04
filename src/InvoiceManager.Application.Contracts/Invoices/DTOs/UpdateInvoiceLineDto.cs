using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.Invoices.DTOs;

public class UpdateInvoiceLineDto
{
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(typeof(decimal), "0,01", "999999999999")]
    public decimal UnitPrice { get; set; }
}

using Volo.Abp.Application.Dtos;

namespace InvoiceManager.Invoices.DTOs
{
    public class GetInvoicesInput : PagedAndSortedResultRequestDto
    {
        public string? InvoiceNumberFilter { get; set; }
        public InvoiceState? State { get; set; }
    }
}

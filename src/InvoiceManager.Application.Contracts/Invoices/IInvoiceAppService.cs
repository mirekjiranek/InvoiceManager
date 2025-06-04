using InvoiceManager.Invoices.DTOs;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace InvoiceManager.Invoices;
public interface IInvoiceAppService
{
    Task<InvoiceDto> GetAsync(Guid id);
    Task<PagedResultDto<InvoiceDto>> GetListAsync(GetInvoicesInput input);
    Task<InvoiceDto> CreateAsync(CreateInvoiceDto input);
    Task<InvoiceDto> AddLineAsync(Guid invoiceId, AddInvoiceLineDto input);
    Task<InvoiceDto> UpdateLineAsync(Guid invoiceId, Guid lineId, UpdateInvoiceLineDto input);
    Task DeleteLineAsync(Guid invoiceId, Guid lineId);
    Task<InvoiceDto> ApproveAsync(Guid id);
    Task<InvoiceDto> PayAsync(Guid id);
}

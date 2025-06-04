using InvoiceManager.Invoices.DTOs;
using InvoiceManager.Repositories;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace InvoiceManager.Invoices;

public class InvoiceAppService : InvoiceManagerAppService, IInvoiceAppService
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceAppService(
        IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    #region Public acces

    [AllowAnonymous]
    public async Task<InvoiceDto> GetAsync(Guid id)
    {
        var invoice = await _invoiceRepository.GetByIdIncludeDetailsAsync(id);
        if (invoice == null)
        {
            throw new EntityNotFoundException(typeof(Invoice), id);
        }

        return ObjectMapper.Map<Invoice, InvoiceDto>(invoice);
    }

    [AllowAnonymous]
    public async Task<PagedResultDto<InvoiceDto>> GetListAsync(GetInvoicesInput input)
    {
        var totalCount = await _invoiceRepository.GetCountAsync();

        var invoices = await _invoiceRepository.GetFilteredListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.InvoiceNumberFilter,
            input.State);

        return new PagedResultDto<InvoiceDto>
        {
            TotalCount = totalCount, 
            Items = ObjectMapper.Map<List<Invoice>, List<InvoiceDto>>(invoices)
        };
    }

    #endregion

    #region Authorized acces

    [Authorize]
    public async Task<InvoiceDto> CreateAsync(CreateInvoiceDto input)
    {
        var existingInvoice = await _invoiceRepository
            .SingleOrDefaultAsync(x => x.InvoiceNumber == input.InvoiceNumber);

        if (existingInvoice != null)
        {
            throw new BusinessException(null, $"Invoice with number '{input.InvoiceNumber}' already exists");
        }

        var invoice = new Invoice(GuidGenerator.Create(), input.InvoiceNumber, input.IssueDate);
        await _invoiceRepository.InsertAsync(invoice);
        return ObjectMapper.Map<Invoice, InvoiceDto>(invoice);
    }

    [Authorize]
    public async Task<InvoiceDto> AddLineAsync(Guid invoiceId, AddInvoiceLineDto input)
    {
        var invoice = await _invoiceRepository.GetByIdIncludeDetailsAsync(invoiceId);
        if (invoice == null)
        {
            throw new EntityNotFoundException(typeof(Invoice), invoiceId);
        }

        invoice.AddLine(input.ProductName, input.Quantity, input.UnitPrice);
        await _invoiceRepository.UpdateAsync(invoice);
        return ObjectMapper.Map<Invoice, InvoiceDto>(invoice);
    }

    [Authorize]
    public async Task<InvoiceDto> UpdateLineAsync(Guid invoiceId, Guid productId, UpdateInvoiceLineDto input)
    {
        var invoice = await _invoiceRepository.GetByIdIncludeDetailsAsync(invoiceId);
        if (invoice == null)
        {
            throw new EntityNotFoundException(typeof(Invoice), invoiceId);
        }

        invoice.UpdateLine(productId, input.Quantity, input.UnitPrice);
        await _invoiceRepository.UpdateAsync(invoice);
        return ObjectMapper.Map<Invoice, InvoiceDto>(invoice);
    }

    [Authorize]
    public async Task DeleteLineAsync(Guid invoiceId, Guid productId)
    {
        var invoice = await _invoiceRepository.GetByIdIncludeDetailsAsync(invoiceId);
        if (invoice == null)
        {
            throw new EntityNotFoundException(typeof(Invoice), invoiceId);
        }

        invoice.RemoveLine(productId);
        await _invoiceRepository.UpdateAsync(invoice);
    }

    [Authorize]
    public async Task<InvoiceDto> ApproveAsync(Guid id)
    {
        var invoice = await _invoiceRepository.GetByIdIncludeDetailsAsync(id);
        if (invoice == null)
        {
            throw new EntityNotFoundException(typeof(Invoice), id);
        }

        invoice.Approve();
        await _invoiceRepository.UpdateAsync(invoice);
        return ObjectMapper.Map<Invoice, InvoiceDto>(invoice);
    }

    [Authorize]
    public async Task<InvoiceDto> PayAsync(Guid id)
    {
        var invoice = await _invoiceRepository.GetAsync(id);
        if (invoice == null)
        {
            throw new EntityNotFoundException(typeof(Invoice), id);
        }

        invoice.Pay();
        await _invoiceRepository.UpdateAsync(invoice);
        return ObjectMapper.Map<Invoice, InvoiceDto>(invoice);
    }

    #endregion
}

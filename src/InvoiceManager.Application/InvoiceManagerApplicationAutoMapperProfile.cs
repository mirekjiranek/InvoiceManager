using AutoMapper;
using InvoiceManager.Invoices;
using InvoiceManager.Invoices.DTOs;

namespace InvoiceManager;

public class InvoiceManagerApplicationAutoMapperProfile : Profile
{
    public InvoiceManagerApplicationAutoMapperProfile()
    {
        CreateMap<Invoice, InvoiceDto>();
        CreateMap<InvoiceLine, InvoiceLineDto>();
    }
}

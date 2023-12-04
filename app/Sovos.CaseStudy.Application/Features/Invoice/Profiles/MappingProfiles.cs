using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Sovos.CaseStudy.Application.Features.Invoice.Commands.CreateInvoiceCommand;
using Sovos.CaseStudy.Application.Features.Invoice.Dto;
using Sovos.CaseStudy.Application.Features.Invoice.Models;
using Sovos.CaseStudy.Domain.Entites;

namespace Sovos.CaseStudy.Application.Features.Invoice.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateInvoiceRequest, InvoiceHeader>()
            .ForMember(dest => dest.InvoiceLine, opt => opt.MapFrom(src => src.InvoiceLine)).ReverseMap();
        CreateMap<CreateInvoiceLineRequest, InvoiceLine>().ReverseMap();
        CreateMap<InvoiceLine, CreateInvoiceLineDto>();
        CreateMap<InvoiceHeader, CreateInvoiceDto>().ReverseMap();
        CreateMap<InvoiceHeader, GetInvoiceHeaderQueryModel>().ForMember(x => x.InvoiceLine, c => c.MapFrom(src => src.InvoiceLine)).ReverseMap();
        CreateMap<IPaginate<InvoiceHeader>, GetListresponse<GetInvoiceHeaderQueryModel>>().ReverseMap();
        CreateMap<InvoiceLine, GetInvoiceLineQueryModel>();
    }
}

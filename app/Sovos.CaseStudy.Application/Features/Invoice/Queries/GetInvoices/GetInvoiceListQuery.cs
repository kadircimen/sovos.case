using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sovos.CaseStudy.Application.Features.Invoice.Models;
using Sovos.CaseStudy.Application.Services;
using Sovos.CaseStudy.Domain.Entites;

namespace Sovos.CaseStudy.Application.Features.Invoice.Queries.GetInvoices;
public class GetInvoiceListQuery : IRequest<GetListresponse<GetInvoiceHeaderQueryModel>>
{
    public PageRequest PageRequest { get; set; }
    public class GetInvoiceListQueryHandler : IRequestHandler<GetInvoiceListQuery, GetListresponse<GetInvoiceHeaderQueryModel>>
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IMapper _mapper;
        public GetInvoiceListQueryHandler(IInvoiceRepository invoiceRepo, IMapper mapper)
        {
            _invoiceRepo = invoiceRepo;
            _mapper = mapper;
        }
        public async Task<GetListresponse<GetInvoiceHeaderQueryModel>> Handle(GetInvoiceListQuery request, CancellationToken cancelToken)
        {
            IPaginate<InvoiceHeader> invoices = await _invoiceRepo.GetListAsync(
                index: request.PageRequest.Page, size: request.PageRequest.PageSize, include: x => x.Include(c => c.InvoiceLines));

            var model = _mapper.Map<GetListresponse<GetInvoiceHeaderQueryModel>>(invoices);
            return model;
        }
    }
}

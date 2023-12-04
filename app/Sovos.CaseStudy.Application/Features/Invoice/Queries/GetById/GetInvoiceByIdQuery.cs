using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sovos.CaseStudy.Application.Features.Invoice.Models;
using Sovos.CaseStudy.Application.Services;

namespace Sovos.CaseStudy.Application.Features.Invoice.Queries.GetById;
public class GetInvoiceByIdQuery : IRequest<GetInvoiceHeaderQueryModel>
{
    public int Id { get; set; }
    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, GetInvoiceHeaderQueryModel>
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IMapper _mapper;
        public GetInvoiceByIdQueryHandler(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepo = invoiceRepository;
            _mapper = mapper;
        }
        public async Task<GetInvoiceHeaderQueryModel> Handle(GetInvoiceByIdQuery request, CancellationToken cancelToken)
        {
            var data = await _invoiceRepo.GetAsync(x => x.Id == request.Id, include: c => c.Include(v => v.InvoiceLine));
            var model = _mapper.Map<GetInvoiceHeaderQueryModel>(data);
            return model;
        }
    }
}

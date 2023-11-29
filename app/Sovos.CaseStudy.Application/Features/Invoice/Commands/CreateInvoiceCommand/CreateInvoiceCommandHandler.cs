using AutoMapper;
using MediatR;
using Sovos.CaseStudy.Application.Features.Invoice.Dto;
using Sovos.CaseStudy.Application.Features.Invoice.Rules;
using Sovos.CaseStudy.Application.Services;
using Sovos.CaseStudy.Domain.Entites;

namespace Sovos.CaseStudy.Application.Features.Invoice.Commands.CreateInvoiceCommand;
public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceRequest, CreateInvoiceDto>
{
    private readonly IInvoiceRepository _invoiceRepo;
    private readonly IMapper _mapper;
    private readonly InvoiceBusinessRules _rules;
    public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepo, IMapper mapper, InvoiceBusinessRules rules)
    {
        _invoiceRepo = invoiceRepo;
        _mapper = mapper;
        _rules = rules;
    }
    public async Task<CreateInvoiceDto> Handle(CreateInvoiceRequest request, CancellationToken cancelToken)
    {
        InvoiceHeader mapped = _mapper.Map<InvoiceHeader>(request);
        var invoiceLines = _mapper.Map<List<InvoiceLine>>(request.InvoiceLines);
        InvoiceHeader created = await _invoiceRepo.AddAsyncWithRelation(mapped, entity =>
        {
            foreach (var line in invoiceLines)
            {
                entity.InvoiceLines.Add(line);
            }
        });
        CreateInvoiceDto dto = _mapper.Map<CreateInvoiceDto>(created);
        return dto;
    }
}

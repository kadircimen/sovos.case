using MediatR;
using Sovos.CaseStudy.Application.Features.Invoice.Dto;
using Sovos.CaseStudy.Domain.Entites;

namespace Sovos.CaseStudy.Application.Features.Invoice.Commands.CreateInvoiceCommand;
public class CreateInvoiceRequest : IRequest<CreateInvoiceDto>
{
    public string InvoiceId { get; set; }
    public string SenderTitle { get; set; }
    public string ReceiverTitle { get; set; }
    public string Date { get; set; }
    public string Email { get; set; }
    public List<CreateInvoiceLineRequest> InvoiceLine { get; set; }
}
public class CreateInvoiceLineRequest
{
    public int InvoiceHeaderId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string UnitCode { get; set; }
    public int UnitPrice { get; set; }
}

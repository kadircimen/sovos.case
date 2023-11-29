using Sovos.CaseStudy.Application.Features.Invoice.Commands.CreateInvoiceCommand;

namespace Sovos.CaseStudy.Application.Features.Invoice.Dto;
public class CreateInvoiceDto
{
    public string InvoiceId { get; set; }
    public string SenderTitle { get; set; }
    public string ReceiverTitle { get; set; }
    public string SendDate { get; set; }
    public string Email { get; set; }
    public List<CreateInvoiceLineDto> InvoiceLines { get; set; }
}
public class CreateInvoiceLineDto
{
    public int InvoiceHeaderId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string UnitCode { get; set; }
    public int UnitPrice { get; set; }
}

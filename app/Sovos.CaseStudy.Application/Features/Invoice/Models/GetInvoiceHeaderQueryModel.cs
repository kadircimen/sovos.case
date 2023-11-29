namespace Sovos.CaseStudy.Application.Features.Invoice.Models;
public class GetInvoiceHeaderQueryModel
{
    public int Id { get; set; }
    public string InvoiceId { get; set; }
    public string SenderTitle { get; set; }
    public string ReceiverTitle { get; set; }
    public string SendDate { get; set; }
    public string Email { get; set; }
    public bool IsMailSended { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } = DateTime.Now;
    public bool IsActive { get; set; }
    public virtual List<GetInvoiceLineQueryModel> InvoiceLines { get; set; }
}
public class GetInvoiceLineQueryModel
{
    public int Id { get; set; }
    public int InvoiceHeaderId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string UnitCode { get; set; }
    public int UnitPrice { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } = DateTime.Now;
    public bool IsActive { get; set; }
}

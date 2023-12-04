using Core.Persistence.Repositories;
namespace Sovos.CaseStudy.Domain.Entites;
public class InvoiceHeader : BaseEntity
{
    public string InvoiceId { get; set; }
    public string SenderTitle { get; set; }
    public string ReceiverTitle { get; set; }
    public string Date { get; set; }
    public string Email { get; set; }
    public bool IsMailSended { get; set; }
    public virtual ICollection<InvoiceLine> InvoiceLine { get; set; }
}

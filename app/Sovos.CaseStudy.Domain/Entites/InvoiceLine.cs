using Core.Persistence.Repositories;
namespace Sovos.CaseStudy.Domain.Entites;
public class InvoiceLine : BaseEntity
{
    public int InvoiceHeaderId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string UnitCode { get; set; }
    public int UnitPrice { get; set; }
}

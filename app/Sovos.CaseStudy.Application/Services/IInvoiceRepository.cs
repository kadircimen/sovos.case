using Core.Persistence.Repositories;
using Sovos.CaseStudy.Domain.Entites;
namespace Sovos.CaseStudy.Application.Services;
public interface IInvoiceRepository : IAsyncRepository<InvoiceHeader>
{
}

using Core.Persistence.Repositories;
using Sovos.CaseStudy.Application.Services;
using Sovos.CaseStudy.Domain.Entites;
using Sovos.CaseStudy.Persistence.Contexts;

namespace Sovos.CaseStudy.Persistence.Repositories;
public class InvoiceRepository : EfRepositoryBase<InvoiceHeader, SovosCaseDbContext>, IInvoiceRepository
{
    public InvoiceRepository(SovosCaseDbContext dbContext) : base(dbContext) { }
}


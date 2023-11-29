using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sovos.CaseStudy.Application.Services;
using Sovos.CaseStudy.Persistence.Contexts;
using Sovos.CaseStudy.Persistence.Repositories;
namespace Sovos.CaseStudy.Persistence;
public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SovosCaseDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("SovosDbConnection")));
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        return services;
    }
}


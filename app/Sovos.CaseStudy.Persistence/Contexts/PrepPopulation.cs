using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sovos.CaseStudy.Persistence.Contexts;

namespace ItWorked.Persistence.Contexts;
public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var srv = scope.ServiceProvider;
            var context = srv.GetRequiredService<SovosCaseDbContext>();
            //context.Database.Migrate();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sovos.CaseStudy.Domain.Entites;

namespace Sovos.CaseStudy.Persistence.Contexts;
public class SovosCaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
    public DbSet<InvoiceLine> InvoiceLine { get; set; }
    public SovosCaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<InvoiceHeader>(x =>
        {
            x.ToTable("InvoiceHeaders").HasKey(c => c.Id);
            x.HasMany(c => c.InvoiceLine);
        });

        modelBuilder.Entity<InvoiceLine>(x =>
        {
            x.ToTable("Invoice Lines").HasKey(c => c.Id);
        });
    }
}

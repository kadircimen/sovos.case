using Core.CrossCuttingConcerns.Exceptions;
using Hangfire;
using Hangfire.PostgreSql;
using ItWorked.Persistence.Contexts;
using Sovos.CaseStudy.Application;
using Sovos.CaseStudy.Application.Services;
using Sovos.CaseStudy.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddHangfire(cnf =>
cnf.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
.UseSimpleAssemblyNameTypeSerializer()
.UseRecommendedSerializerSettings()
.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("SovosDbConnection"), new PostgreSqlStorageOptions
{
    QueuePollInterval = TimeSpan.FromSeconds(30),
    InvisibilityTimeout = TimeSpan.FromSeconds(30),
    TransactionSynchronisationTimeout = TimeSpan.FromMinutes(1),
    SchemaName = "Hangfire_Schema",
    PrepareSchemaIfNecessary = true
}));
builder.Services.AddHangfireServer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();
var recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.CustomExceptionMiddleware();
app.UseHangfireDashboard();
recurringJobManager.AddOrUpdate<SendInvoiceMailJob>("send-invoice-mail",
    job => job.Execute(),
    "*/1 * * * *");
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
PrepDb.PrepPopulation(app);
app.Run();

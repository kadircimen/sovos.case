using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Validation;
using Core.CrossCuttingConcerns.Logging;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sovos.CaseStudy.Application.Features.Invoice.Rules;
using System.Reflection;

namespace Sovos.CaseStudy.Application;
public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<InvoiceBusinessRules>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(conf =>
        {
            conf.AddBehavior(typeof(IPipelineBehavior<,>), (typeof(ValidationBehavior<,>)));
            conf.AddBehavior(typeof(IPipelineBehavior<,>), (typeof(LoggingBehavior<,>)));
            conf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        services.AddSingleton<LoggerServiceBase, ElasticSearchLogger>();
        return services;
    }
}


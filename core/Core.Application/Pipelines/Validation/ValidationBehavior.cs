using Elasticsearch.Net.Specification.WatcherApi;
using FluentValidation;
using MediatR;

namespace Core.Application.Pipelines.Validation;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResult = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));
            var fails = validationResult.SelectMany(x => x.Errors).Where(x => x != null).ToList();
            if (fails.Count > 0)
                throw new FluentValidation.ValidationException(fails);
        }
        return await next();
    }
}
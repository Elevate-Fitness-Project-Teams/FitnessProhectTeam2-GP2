using FluentValidation;
using MediatR;
using Elevate.Nutrition.Domain.Common;

namespace Elevate.Nutrition.Application.Features;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (failures.Count == 0)
            return await next();

        var error = string.Join("; ", failures.Select(f => f.ErrorMessage));
        var resultType = typeof(TResponse);

        if (resultType.IsGenericType)
        {
            var genericArg = resultType.GetGenericArguments()[0];
            var failureMethod = typeof(Result)
                .GetMethods()
                .First(m => m.Name == "Failure" && m.IsGenericMethod)
                .MakeGenericMethod(genericArg);
            return (TResponse)failureMethod.Invoke(null, new object[] { error })!;
        }

        return (TResponse)Result.Failure(error);
    }
}

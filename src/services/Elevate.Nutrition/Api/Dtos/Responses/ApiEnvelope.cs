using System.Net;
using Elevate.Nutrition.Domain.Common;

namespace Elevate.Nutrition.Api.Dtos.Responses;

public class ApiEnvelope
{
    public bool IsSuccess { get; init; }
    public object? Data { get; init; }
    public List<string> Errors { get; init; } = new();
    public int StatusCode { get; init; }

    public static ApiEnvelope Success(object? data, HttpStatusCode statusCode = HttpStatusCode.OK)
        => new()
        {
            IsSuccess = true,
            Data = data,
            StatusCode = (int)statusCode
        };

    public static ApiEnvelope Failure(List<string> errors,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new()
        {
            IsSuccess = false,
            Errors = errors,
            StatusCode = (int)statusCode
        };

    public static ApiEnvelope FromResult(Result result)
    {
        if (result.IsSuccess)
        {
            var data = result is Result<object> r ? r.Value : null;
            return Success(data);
        }

        return Failure(new List<string> { result.Error! });
    }
}

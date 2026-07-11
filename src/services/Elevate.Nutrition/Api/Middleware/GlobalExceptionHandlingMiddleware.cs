using System.Net;
using System.Text.Json;
using Elevate.Nutrition.Api.Dtos.Responses;

namespace Elevate.Nutrition.Api.Middleware;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled system exception: {Message}", ex.Message);

            var envelope = ApiEnvelope.Failure(
                new List<string> { "An unexpected system error occurred." },
                HttpStatusCode.InternalServerError);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(JsonSerializer.Serialize(envelope));
        }
    }
}

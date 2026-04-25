using System.Net;
using System.Text.Json;

namespace HomeGrown.API.Middleware;

/// <summary>
/// Global exception handler — catches all unhandled exceptions and returns
/// a consistent JSON error response. Prevents stack traces leaking to clients.
/// </summary>
public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Unauthorized."),
            KeyNotFoundException        => (HttpStatusCode.NotFound, exception.Message),
            ArgumentException          => (HttpStatusCode.BadRequest, exception.Message),
            InvalidOperationException  => (HttpStatusCode.BadRequest, exception.Message),
            _                          => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
        };

        context.Response.StatusCode = (int)statusCode;

        var payload = JsonSerializer.Serialize(new { error = message });
        return context.Response.WriteAsync(payload);
    }
}

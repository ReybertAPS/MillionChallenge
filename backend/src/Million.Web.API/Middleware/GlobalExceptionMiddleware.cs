using FluentValidation;
using Million.Web.API.Contracts.Errors;

namespace Million.Web.API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            var response = new ApiErrorResponse(400, "Validation failed", ex.Errors.Select(e => e.ErrorMessage));
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (KeyNotFoundException ex)
        {
            var response = new ApiErrorResponse(404, "Not found", new[] { ex.Message });
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            var response = new ApiErrorResponse(500, "Internal server error", new[] { ex.Message });
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

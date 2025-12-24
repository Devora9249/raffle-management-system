using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace server.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            var (statusCode, title) = ex switch
            {
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Not Found"),
                ArgumentException => (StatusCodes.Status400BadRequest, "Bad Request"),
                InvalidOperationException => (StatusCodes.Status409Conflict, "Conflict"),
                _ => (StatusCodes.Status500InternalServerError, "Server Error")
            };

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = statusCode == 500 ? "An unexpected error occurred." : ex.Message,
                Instance = context.Request.Path
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}

using FluentValidation;
using TiRoDev.DotNet.Scaffolds.All.Application.Validation;

namespace TiRoDev.DotNet.Scaffolds.All.Api.Middlewares;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException validationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(validationException.ToProblemDetails());
        }
    }
}
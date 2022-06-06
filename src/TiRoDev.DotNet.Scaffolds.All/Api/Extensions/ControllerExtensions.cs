using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using TiRoDev.DotNet.Scaffolds.All.Application.Validation;

namespace TiRoDev.DotNet.Scaffolds.All.Api.Extensions;

public static class ControllerExtensions
{
    public static IActionResult ToResponse<TResult, TContract>(this Result<TResult> result, Func<TResult, TContract> mapper)
    {
        return result.Match<IActionResult>(obj =>
        {
            var response = mapper(obj);
            return new OkObjectResult(response);
        }, exception =>
        {
            if (exception is ValidationException validationException)
            {
                return new BadRequestObjectResult(validationException.ToProblemDetails());
            }
            
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        });
    }
}
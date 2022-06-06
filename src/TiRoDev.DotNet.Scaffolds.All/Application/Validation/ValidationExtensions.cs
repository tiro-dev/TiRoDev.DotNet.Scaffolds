using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TiRoDev.DotNet.Scaffolds.All.Application.Validation;

public static class ValidationExtensions
{
    public static ValidationProblemDetails ToProblemDetails(this ValidationException validationException)
    {
        var problemDetails = new ValidationProblemDetails
        {
            Title = "One or more validation errors occurred.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Status = StatusCodes.Status400BadRequest
        };
        
        foreach (var validationFailure in validationException.Errors)
        {
            if (problemDetails.Errors.ContainsKey(validationFailure.PropertyName))
            {
                problemDetails.Errors[validationFailure.PropertyName] = 
                    problemDetails.Errors[validationFailure.PropertyName].Concat(new[] { validationFailure.ErrorMessage }).ToArray();
                continue;
            }
            
            problemDetails.Errors.Add(new KeyValuePair<string, string[]>(
                validationFailure.PropertyName,
                new []{ validationFailure.ErrorMessage }));
        }
        
        return problemDetails;
    }
}
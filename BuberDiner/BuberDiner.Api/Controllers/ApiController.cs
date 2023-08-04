using BuberDiner.Api.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BuberDiner.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem(modelStateDictionary);
        }

        HttpContext.Items.Add(HttpContextItemKeys.Errors, errors);
        var firstError = errors.First();

        var statusCode = firstError.Type switch {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Failure => throw new NotImplementedException(),
            ErrorType.Unexpected => throw new NotImplementedException(),
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, detail: firstError.Description);
    }

}

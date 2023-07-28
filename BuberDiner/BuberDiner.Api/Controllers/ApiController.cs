using BuberDiner.Api.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDiner.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
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

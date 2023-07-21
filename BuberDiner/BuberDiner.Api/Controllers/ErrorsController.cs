using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDiner.Api.Controllers;

public class ErrorsController : ControllerBase
{

    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;
        return Problem(title: exception!.Message, statusCode: 500);
    }
}

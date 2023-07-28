using BuberDiner.Contracts.Authentication;
using BuberDiner.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using BuberDiner.Application.Authentication.Commands.Register;
using BuberDiner.Application.Authentication.Queries.Login;
using BuberDiner.Application.Authentication.Common;

namespace BuberDiner.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        var authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(MapAuthResponse(authResult)),
            errors => Problem(errors)
        );
    }

    private static AuthenticationResponse MapAuthResponse(AuthenticationResult result)
    {
        return new AuthenticationResponse
                    (
                        result.User.Id,
                        result.User.FirstName,
                        result.User.LastName,
                        result.User.Email,
                        result.Token
                    );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = new LoginCommand(request.Email, request.Password);
        var authResult = await _mediator.Send(command);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                detail: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(MapAuthResponse(authResult)),
            errors => Problem(errors)
        );
    }
}
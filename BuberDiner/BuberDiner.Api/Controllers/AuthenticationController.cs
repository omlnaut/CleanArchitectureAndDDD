using BuberDiner.Contracts.Authentication;
using BuberDiner.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using BuberDiner.Application.Services.Authentication.Common;
using BuberDiner.Application.Services.Authentication.Queries;

namespace BuberDiner.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationQueryService _authenticationService;

    public AuthenticationController(IAuthenticationQueryService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

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
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(
            request.Email,
            request.Password);

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
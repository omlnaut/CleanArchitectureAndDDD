using BuberDiner.Contracts.Authentication;
using BuberDiner.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using BuberDiner.Application.Services.Authentication.Common;
using BuberDiner.Application.Services.Authentication.Queries;
using BuberDiner.Application.Services.Authentication.Commands;

namespace BuberDiner.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationQueryService _authenticationQueryService;
    private readonly IAuthenticationCommandService _authenticationCommandService;

    public AuthenticationController(IAuthenticationQueryService authenticationService, IAuthenticationCommandService authenticationCommandService)
    {
        _authenticationQueryService = authenticationService;
        _authenticationCommandService = authenticationCommandService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationCommandService.Register(
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
        var authResult = _authenticationQueryService.Login(
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
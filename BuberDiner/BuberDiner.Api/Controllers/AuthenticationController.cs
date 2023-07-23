using BuberDiner.Contracts.Authentication;
using BuberDiner.Application.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDiner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
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

        return authResult.MatchFirst(
            authResult => Ok(MapAuthResponse(authResult)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
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

        return authResult.MatchFirst(
            authResult => Ok(MapAuthResponse(authResult)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );
    }
}
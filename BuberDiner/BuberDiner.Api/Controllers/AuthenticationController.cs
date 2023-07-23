using BuberDiner.Contracts.Authentication;
using BuberDiner.Application.Services.Authentication;
using Microsoft.AspNetCore.Mvc;
using BuberDiner.Application.Common.Errors;

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
        var registerResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        if (registerResult.IsSuccess)
        {
            return Ok(MapAuthResult(registerResult.Value));
        }

        var firstError = registerResult.Errors.First();

        if (firstError is DuplicateEmailError)
        {
            return Conflict("Email already exists");
        }

        return Problem();
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse
        (
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(
            request.Email,
            request.Password);

        var response = new AuthenticationResponse
        (
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token

        );
        return Ok(response);
    }
}

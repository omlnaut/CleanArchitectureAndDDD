using BuberDiner.Application.Services.Authentication.Common;
using ErrorOr;

namespace BuberDiner.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
    public ErrorOr<AuthenticationResult> Login(string email, string password);
}

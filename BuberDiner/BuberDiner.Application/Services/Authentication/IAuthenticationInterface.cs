namespace BuberDiner.Application.Services.Authentication;

public interface IAuthenticationInterface
{
    public AuthenticationResult Login(string firstName, string lastName, string email, string password);
    public AuthenticationResult Register(string email, string password);
}

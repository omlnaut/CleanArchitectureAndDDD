using FluentValidation;

namespace BuberDiner.Application.Authentication.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        _ = RuleFor(x => x.Email).NotEmpty();
        _ = RuleFor(x => x.Password).NotEmpty();
    }
}

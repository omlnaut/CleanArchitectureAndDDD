using BuberDiner.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BuberDiner.Application.Authentication.Queries.Login;

public record LoginCommand(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
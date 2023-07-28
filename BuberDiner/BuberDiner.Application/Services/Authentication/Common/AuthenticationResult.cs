using BuberDiner.Domain.Entities;

namespace BuberDiner.Application.Services.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);
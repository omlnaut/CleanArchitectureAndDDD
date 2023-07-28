using BuberDiner.Application.Services.Authentication.Commands;
using BuberDiner.Application.Services.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDiner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        _ = services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        _ = services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
        return services;
    }
}

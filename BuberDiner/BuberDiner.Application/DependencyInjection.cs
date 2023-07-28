using BuberDiner.Application.Services.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDiner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        return services;
    }
}

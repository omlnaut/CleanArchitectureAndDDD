using BuberDiner.Application.Common.Interfaces.Authentication;
using BuberDiner.Application.Common.Interfaces.Persistence;
using BuberDiner.Application.Common.Interfaces.Services;
using BuberDiner.Infrastructure.Authentication;
using BuberDiner.Infrastructure.Persistence;
using BuberDiner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDiner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    ConfigurationManager configuration)
    {
        _ = services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        _ = services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        _ = services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        _ = services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}

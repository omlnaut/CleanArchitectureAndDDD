using BuberDiner.Api.Common.Errors;
using BuberDiner.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BuberDiner.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        _ = services.AddControllers();
        _ = services.AddSingleton<ProblemDetailsFactory, BuberDinerProblemDetailsFactory>();
        _ = services.AddMapping();

        return services;
    }
}

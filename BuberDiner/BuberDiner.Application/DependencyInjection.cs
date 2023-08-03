using BuberDiner.Application.Authentication.Commands.Register;
using BuberDiner.Application.Authentication.Common;
using BuberDiner.Application.Common.Behaviors;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDiner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        _ = services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));
        _ = services.AddScoped<
                IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>,
                ValidationBehavior>();

        return services;
    }
}

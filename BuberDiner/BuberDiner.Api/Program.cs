using BuberDiner.Api;
using BuberDiner.Api.Common.Errors;
using BuberDiner.Application;
using BuberDiner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

{
    _ = builder.Services
            .AddPresentation()
            .AddApplication()
            .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();

{
    _ = app.UseExceptionHandler("/error");

    _ = app.UseHttpsRedirection();
    _ = app.MapControllers();
    app.Run();
}

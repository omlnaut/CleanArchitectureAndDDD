using BuberDiner.Api.Common.Errors;
using BuberDiner.Application;
using BuberDiner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

{
    _ = builder.Services.AddApplication();
    _ = builder.Services.AddInfrastructure(builder.Configuration);

    _ = builder.Services.AddControllers();

    _ = builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinerProblemDetailsFactory>();
}

var app = builder.Build();

{
    _ = app.UseExceptionHandler("/error");

    _ = app.UseHttpsRedirection();
    _ = app.MapControllers();
    app.Run();
}

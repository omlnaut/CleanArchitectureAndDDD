using BuberDiner.Api.Filters;
using BuberDiner.Application;
using BuberDiner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

{
    _ = builder.Services.AddApplication();
    _ = builder.Services.AddInfrastructure(builder.Configuration);
    _ = builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
}

var app = builder.Build();

{
    // _ = app.UseMiddleware<ErrorHandlingMiddleware>();
    _ = app.UseHttpsRedirection();
    _ = app.MapControllers();
    app.Run();
}

using Presence.Application;
using Presence.Persistence;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(config =>
{
    config.ReadFrom.Configuration(builder.Configuration);
    config.Enrich.FromLogContext();
});

builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration);

WebApplication app = builder.Build();

app.Run();
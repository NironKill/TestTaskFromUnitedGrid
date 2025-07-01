using Notification.Application;
using Notification.Infrastructure;
using Serilog;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSerilog(config =>
{
    config.ReadFrom.Configuration(builder.Configuration);
    config.Enrich.FromLogContext();
});

builder.Services
    .AddApplication()
    .AddInfrastructure();

IHost host = builder.Build();

host.Run();
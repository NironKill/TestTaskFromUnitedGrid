using Notification.Infrastructure;
using Notification.Infrastructure.Email.Options;
using Serilog;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSerilog(config =>
{
    config.ReadFrom.Configuration(builder.Configuration);
    config.Enrich.FromLogContext();
});

builder.Services.Configure<EmailOption>(builder.Configuration.GetSection("Email"));

builder.Services
    .AddInfrastructure();

IHost host = builder.Build();

host.Run();
using Ocelot.DependencyInjection;
using Ocelot.Provider.Kubernetes;
using Ocelot.Cache.CacheManager;
using Serilog;
using Ocelot.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile($"ocelot.json", true, true)
    .AddJsonFile($"ocelot.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);

builder.Services.AddSerilog(config =>
{
    config.ReadFrom.Configuration(builder.Configuration);
    config.Enrich.FromLogContext();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy => { policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
});

builder.Services
    .AddOcelot()
    .AddKubernetes()
    .AddCacheManager(o => o.WithDictionaryHandle());

WebApplication app = builder.Build();

app.UseRouting();
app.UseCors("CorsPolicy");

await app.UseOcelot();

app.Run();
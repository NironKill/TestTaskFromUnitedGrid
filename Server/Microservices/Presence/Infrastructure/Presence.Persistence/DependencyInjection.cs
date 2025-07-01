using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presence.Persistence.Settings;

namespace Presence.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DataBaseSet>(configuration.GetSection(DataBaseSet.Configuration));
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Connection.GetOptionConfiguration(
                    configuration.GetSection(DataBaseSet.Configuration).Get<DataBaseSet>().ConnectionString);
            });

            return services;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UGMessenger.Application.Interfaces;
using UGMessenger.Persistence.Common;
using UGMessenger.Persistence.Settings;

namespace UGMessenger.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DataBaseSet>(configuration.GetSection(DataBaseSet.Configuration));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(Connection.GetOptionConfiguration(
                configuration.GetSection(DataBaseSet.Configuration).Get<DataBaseSet>().ConnectionString)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Presence.Application.Services;

namespace Presence.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IPresenceService, PresenceService>();

            return services;
        }
    }
}

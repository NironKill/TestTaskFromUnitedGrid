using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UGMessenger.Infrastructure.Settings;

namespace UGMessenger.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("Gateway", client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection(APIGatewaySet.Configuration).Get<APIGatewaySet>().URL);
            });

            return services;
        }
    }
}

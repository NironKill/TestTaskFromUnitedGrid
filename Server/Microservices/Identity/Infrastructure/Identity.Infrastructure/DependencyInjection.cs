using Identity.Infrastructure.RabbitMQ.Pub.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBusClient, MessageBusClient>();

            return services;
        }
    }
}

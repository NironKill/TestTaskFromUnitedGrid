using Chat.Infrastructure.RabbitMQ.Processor;
using Chat.Infrastructure.RabbitMQ.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHostedService<MessageBusSubscriber>();

            services.AddSingleton<IEventProcessor, EventProcessor>();

            return services;
        }
    }
}

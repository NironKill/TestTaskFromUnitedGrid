using Chat.Infrastructure.RabbitMQ.Pub.Services;
using Chat.Infrastructure.RabbitMQ.Sub.Processor;
using Chat.Infrastructure.RabbitMQ.Sub.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHostedService<MessageBusSubscriber>();

            services.AddSingleton<IEventProcessor, EventProcessor>();
            services.AddSingleton<IMessageBusClient, MessageBusClient>();
            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Notification.Infrastructure.Email.Services;
using Notification.Infrastructure.RabbitMQ.Sub.Processor;
using Notification.Infrastructure.RabbitMQ.Sub.Services;

namespace Notification.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHostedService<MessageBusSubscriber>();

            services.AddSingleton<IEventProcessor, EventProcessor>();
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }      
    }
}

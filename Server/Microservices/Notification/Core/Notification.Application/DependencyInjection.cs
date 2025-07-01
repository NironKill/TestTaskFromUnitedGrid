using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Services.Implementations;
using Notification.Application.Services.Interfaces;

namespace Notification.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}

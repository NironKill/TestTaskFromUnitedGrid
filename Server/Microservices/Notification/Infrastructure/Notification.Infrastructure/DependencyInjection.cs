using Microsoft.Extensions.DependencyInjection;
using Notification.Infrastructure.Services.Email;

namespace Notification.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }      
    }
}

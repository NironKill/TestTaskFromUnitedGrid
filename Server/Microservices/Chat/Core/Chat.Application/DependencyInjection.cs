using Chat.Application.Repositories.Implementations;
using Chat.Application.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            return services;
        }
    }
}

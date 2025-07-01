using Identity.Application.Repositories.Implementations;
using Identity.Application.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccessTokenRepository, AccessTokenRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}

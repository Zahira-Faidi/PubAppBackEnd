using Authentication.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}

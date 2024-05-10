using Authentication.Application.Common.Interfaces.Authentication;
using Authentication.Application.Common.Interfaces.Persistence;
using Authentication.Application.Common.Interfaces.Services;
using Authentication.Domain.Interface;
using Authentication.Infrastructure.Authentication;
using Authentication.Infrastructure.Persistence.Repositories;
using Authentication.Infrastructure.Security;
using Authentication.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastracture(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHash>();
        return services;
    }
}

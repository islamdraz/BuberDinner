using BuberDinner.Application.Interfaces.Authentication;
using BuberDinner.Application.Interfaces.Persistance;
using BuberDinner.Application.Interfaces.Services;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Persistance;
using BuberDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure;

public static class DepenedencyInjection{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddScoped<IJwtTokenGenerator,JwtTokenGererator>();
        services.AddScoped<IDateTimeProvider,DateTimeProvider>();

        services.AddScoped<IUserRepository,UserRepository>();
        
        return services;
    }
}
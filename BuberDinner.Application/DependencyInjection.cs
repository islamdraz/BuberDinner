using BuberDinner.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application;

public static class DepenedencyInjection{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService,AuthenticationService>();
        return services;
    }
}
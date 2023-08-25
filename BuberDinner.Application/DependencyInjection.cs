using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection.PortableExecutable;
using System.Reflection;

namespace BuberDinner.Application;

public static class DepenedencyInjection{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // // services is not longer used and just keeping this as i don't want to remove the folder 
        // services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
        // services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        
        // services.AddMediatR(typeof(DepenedencyInjection).Assembly);
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}
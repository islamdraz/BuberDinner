using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection.PortableExecutable;
using System.Reflection;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Behaviors;
using ErrorOr;
using FluentValidation;

namespace BuberDinner.Application;

public static class DepenedencyInjection{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
               
        // services.AddMediatR(typeof(DepenedencyInjection).Assembly);
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        // to regiater type like this dynamically 
        // services.AddScoped<IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>, ValidationBehavior> ();

        services.AddScoped(typeof(IPipelineBehavior<,>),
                           typeof(ValidationBehavior<,>));
        // need to make this generic
        // services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>(); 

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
using BuberDinner.Api.Commons.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using  BuberDinner.Api.Commons.Mapping;
using Microsoft.Extensions.DependencyInjection;
namespace BuberDinner.Api;

public static class DepenedencyInjection{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
      services.AddControllers();
       services.AddSingleton<ProblemDetailsFactory,BuberDinnerProblemDetailsFactory>();
       services.AddMappings();
        return services;
    }
}
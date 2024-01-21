using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BuberDinner.Application.Common.Behaviors;

public class ValidationBehavior<TRequest,TResponse> : 
                IPipelineBehavior<TRequest, TResponse>
                where TRequest : IRequest<TResponse>
                where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async  Task<TResponse> Handle(TRequest request,
                                                      RequestHandlerDelegate<TResponse> next,
                                                      CancellationToken cancellationToken)
    {
        if(_validator is null)
        {
            return await next();
        }
        var validationRestult = await _validator.ValidateAsync(request, cancellationToken);

        if(validationRestult.IsValid)
        {
            return await next();
        }

        // .ConvertAll()  = select() then ToList();
        var errors = validationRestult.Errors
                        .ConvertAll(validationFailure => Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage));
                        

       return (dynamic)errors;
    }
}
using BuberDinner.Application.Common.Errors;
using OneOf;

namespace BuberDinner.Application.Services;

public interface IAuthenticationService
{
     AuthenticationResult Login(string email, string password);
    OneOf< AuthenticationResult, IError> Register(string firstName, string lastName, string email, string password);
}

  


using System.Net;

namespace BuberDinner.Application.Common.Errors;

public class DublicateEmailException : Exception, IServiceException
{
    public HttpStatusCode StatusCode =>  HttpStatusCode.Conflict;

    public string ErrorMessage => "Email Already exists";
}
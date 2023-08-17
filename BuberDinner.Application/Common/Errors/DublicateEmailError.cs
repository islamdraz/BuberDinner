using System.Net;

namespace BuberDinner.Application.Common.Errors;


public class DuplicateEmailError : IError
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage => "this Email is not Exists";
}
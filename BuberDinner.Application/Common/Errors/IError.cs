using System.Net;

namespace BuberDinner.Application.Common.Errors;

public interface IError {

    HttpStatusCode StatusCode {get;}

    string ErrorMessage {get;}
}
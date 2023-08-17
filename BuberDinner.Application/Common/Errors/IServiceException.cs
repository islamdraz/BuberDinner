using System.Net;

namespace BuberDinner.Application.Common.Errors;

public interface IServiceException {

    HttpStatusCode StatusCode {get;}

    string ErrorMessage {get;}
}
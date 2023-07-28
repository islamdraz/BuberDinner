using System.Net;
using Microsoft.AspNetCore.Http;

namespace BuberDinner.Application.Common.Errors;

public interface IServiceException {

     HttpStatusCode StatusCode { get; }
     string ErrorMessage {get;}
}
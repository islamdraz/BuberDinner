using BuberDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
      var (statusCode, message) = exception switch 
      {
        IServiceException errorException => ( (int) errorException.StatusCode, errorException.ErrorMessage),
        _ => (StatusCodes.Status500InternalServerError , "An Error Occurred")
      };

        return Problem(statusCode : statusCode,title: message);
    }
}
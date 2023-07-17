using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error1")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem(detail : exception?.Message);
    }
}
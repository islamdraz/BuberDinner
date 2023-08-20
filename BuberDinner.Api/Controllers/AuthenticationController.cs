using BuberDinner.Application.Services;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;


[Route("auth")]
public class AuthenticationController : ApiController
{
    private IAuthenticationService _iAuthenticationService;

    public AuthenticationController(IAuthenticationService iAuthenticationService)
    {
        this._iAuthenticationService = iAuthenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> registerResult = _iAuthenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

      return registerResult.Match(
        result => Ok(MapAuthResult(result)) ,
        errors => Problem(errors)

       );

    }



    [HttpPost("login")]
    // [ErrorHandlingFilter]
    public IActionResult Login(LoginRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _iAuthenticationService.Login(
          request.Email,
          request.Password);

          return authResult.Match(
            result => Ok(MapAuthResult(result)),
            errors => Problem(errors)
          );
      
    }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
    }
}



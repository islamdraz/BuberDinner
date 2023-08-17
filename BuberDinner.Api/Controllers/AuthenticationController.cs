using System.Net;
using BuberDinner.Api.Filters;
using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private IAuthenticationService _iAuthenticationService;

    public AuthenticationController(IAuthenticationService iAuthenticationService)
    {
        this._iAuthenticationService = iAuthenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        OneOf<AuthenticationResult, IError> registerResult = _iAuthenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);


        return registerResult.Match(
              authResult => Ok(MapAuthResult(authResult)),
             error => Problem(statusCode: (int)error.StatusCode, title: error.ErrorMessage)
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

    [HttpPost("login")]
    // [ErrorHandlingFilter]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _iAuthenticationService.Login(
          request.Email,
          request.Password);

        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);

        return Ok(response);
    }
}



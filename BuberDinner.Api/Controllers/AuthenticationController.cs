using BuberDinner.Application.Services;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController:ControllerBase
{
    private IAuthenticationService _iAuthenticationService;

    public AuthenticationController(IAuthenticationService iAuthenticationService)
    {
        this._iAuthenticationService = iAuthenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult= _iAuthenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        var response = new AuthenticationResponse (
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token);

        return Ok(response);
    }
  
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
          var authResult= _iAuthenticationService.Login(         
            request.Email,
            request.Password);

        var response = new AuthenticationResponse (
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token);

        return Ok(response);
    }
}



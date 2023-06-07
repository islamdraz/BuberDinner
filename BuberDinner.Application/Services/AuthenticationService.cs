using System;
using BuberDinner.Application.Interfaces.Authentication;

namespace BuberDinner.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _iJwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator iJwtTokenGenerator)
    {
        _iJwtTokenGenerator = iJwtTokenGenerator;
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // check if user exists

        // create user (generate unique ID)

        // Create Jwt token
        var userId = Guid.NewGuid();
        var token = _iJwtTokenGenerator.GenerateToken(userId, firstName, lastName);
        return new AuthenticationResult(
            userId,
            firstName,
            lastName,
            email,
            token);
    }

    public AuthenticationResult Login(string email, string password)
    {
            var userId = Guid.NewGuid();
          var token = _iJwtTokenGenerator.GenerateToken(userId, "firstName", "lastName");
        return new AuthenticationResult(
            userId,
            "FirstName",
            "LastName",
            email,
            token
        );   
      
    }

}




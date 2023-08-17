using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Interfaces.Authentication;
using BuberDinner.Application.Interfaces.Persistance;
using BuberDinner.Domain.Entities;
using OneOf;

namespace BuberDinner.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _JwtTokenGenerator;
    private readonly IUserRepository _UserRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _JwtTokenGenerator = jwtTokenGenerator;
        _UserRepository = userRepository;
    }

    public OneOf< AuthenticationResult, IError> Register(string firstName, string lastName, string email, string password)
    {
        // 1. validate user is not exists
        if (_UserRepository.GetUserByEmail(email) is not null)
        {
            return new DuplicateEmailError();
        }

        // 2. create user (generate unique ID) & persist to db
        var user = new User
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Password = password
        };
        _UserRepository.Add(user);

        // 3. Create Jwt token

        var token = _JwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        // 1. validate user exists
        if (_UserRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User with given email doesn't exists");
        }

        // 2. validate the password is correct;

        if (user.Password != password)
        {
            throw new Exception("password is not correct");
        }

        var token = _JwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token
        );

    }

}




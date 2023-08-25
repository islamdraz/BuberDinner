using BuberDinner.Application.Interfaces.Authentication;
using BuberDinner.Application.Interfaces.Persistance;
using BuberDinner.Domain.Entities;
using ErrorOr;
using MediatR;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Application.Authentication.Common;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
     private readonly IJwtTokenGenerator _JwtTokenGenerator;
    private readonly IUserRepository _UserRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _JwtTokenGenerator = jwtTokenGenerator;
        _UserRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
         await Task.CompletedTask;
        // 1. validate user exists
        if (_UserRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 2. validate the password is correct;

        if (user.Password != query.Password)
        {
            return new []{ Errors.Authentication.InvalidCredentials};
        }

        var token = _JwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token
        );

    
    }
}
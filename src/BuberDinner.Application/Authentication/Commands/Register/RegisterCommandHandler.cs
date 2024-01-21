using BuberDinner.Application.Interfaces.Authentication;
using BuberDinner.Application.Interfaces.Persistance;
using ErrorOr;
using MediatR;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using BuberDinner.Application.Authentication.Common;

namespace BuberDinner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _JwtTokenGenerator;
    private readonly IUserRepository _UserRepository;

    public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _UserRepository = userRepository;
        _JwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
          if (_UserRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DublicateEmail;
        }

        // 2. create user (generate unique ID) & persist to db
        var user = new User
        {
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Password = command.Password
        };
        _UserRepository.Add(user);

        // 3. Create Jwt token

        var token = _JwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token);
    }


}
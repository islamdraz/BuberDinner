using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
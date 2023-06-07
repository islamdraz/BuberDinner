using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuberDinner.Application.Interfaces.Authentication;
using BuberDinner.Application.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure.Authentication;

public class JwtTokenGererator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private IDateTimeProvider _datetimeProvider;

    public JwtTokenGererator(IDateTimeProvider datetimeProvider,IOptions<JwtSettings> jwtOptions)
    {
        _datetimeProvider = datetimeProvider;
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        string secriet = _jwtSettings.Secret;
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);
            
        
        var claims = new []{
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience:_jwtSettings.Audience,
            expires: _datetimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),            
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
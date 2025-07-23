
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Primrose.Models.Authentication;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Primrose.Auth;

public sealed class JwtProvider(string secret)
{
    private readonly string Secret = secret;

    public string Create(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var descriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                ]
            ),
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = credentials,
        };

        var handler = new JsonWebTokenHandler();
        string token = handler.CreateToken(descriptor);

        return token;
    }
}
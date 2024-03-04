using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dotenv.net;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Lara.Application.API.Utils;

public record UserToken(string Token, DateTime Expiration);

public class JwtToken
{
    public UserToken GenerateToken(string id)
    {
        DotEnv.Load();

        var envVars = DotEnv.Read();

        var jwtToken = envVars["LARA_JWT_KEY"];

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, id)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtToken));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddHours(1);

        var jwtSecToken = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: creds
        );

        return new UserToken(new JwtSecurityTokenHandler().WriteToken(jwtSecToken), expiration);
    }
}
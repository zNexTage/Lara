using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lara.Domain.Contracts;
using Lara.Domain.DataTransferObjects;
using Microsoft.IdentityModel.Tokens;

namespace Lara.Service.Service;

public class JwtService : IBaseTokenService
{
    private readonly string _jwtKey;
    
    public JwtService(string jwtKey)
    {
        _jwtKey = jwtKey;
    }
    public TokenDto GenerateToken(string id)
    {
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, id)
        };

        var symmetricKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtKey)
        );
        
        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.Now.AddHours(6);

        var securityToken = new JwtSecurityToken(
            expires: expires,
            claims: claims,
            signingCredentials: signingCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return new TokenDto()
        {
            Token = token,
            ExpiresIn = expires
        };
    }
}
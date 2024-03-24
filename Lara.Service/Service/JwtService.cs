using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper.Internal;
using Lara.Domain.Contracts;
using Lara.Domain.DataTransferObjects;
using Lara.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Lara.Service.Service;

public class JwtService : IBaseTokenService
{
    private readonly string _jwtKey;
    private readonly UserManager<ApplicationUser> _userManager; 
    
    public JwtService(string jwtKey, UserManager<ApplicationUser> userManager)
    {
        _jwtKey = jwtKey;
        _userManager = userManager;
    }
    public async Task<TokenDto> GenerateToken(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var rolesClaim = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToArray();
        
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        };
        
        claims = claims.Concat(rolesClaim).ToArray();

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
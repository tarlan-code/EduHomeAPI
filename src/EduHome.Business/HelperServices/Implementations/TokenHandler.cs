using EduHome.Business.DTOs.Auth;
using EduHome.Business.HelperServices.Interfaces;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EduHome.Business.HelperServices.Implementations;
public class TokenHandler : ITokenHandler
{

    readonly IConfiguration _configuration;
    readonly UserManager<AppUser> _userManager;
    public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<TokenResponseDto> GenerateTokenAsync(AppUser user,int minute)
    {
     

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Email,user.Email),
        };
        var roles = await _userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            claims.Add(new(ClaimTypes.Role, role));
        }
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken jwtToken = new(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(minute),
            signingCredentials: signingCredentials);

        JwtSecurityTokenHandler tokenHandler = new();
        string token = tokenHandler.WriteToken(jwtToken);

        return new()
        {
            Token = token,
            Username = user.UserName,
            Expires = jwtToken.ValidTo
        };
    }
}

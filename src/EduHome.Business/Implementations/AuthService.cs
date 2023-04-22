using EduHome.Business.DTOs.Auth;
using EduHome.Business.Exceptions;
using EduHome.Business.Interfaces;
using EduHome.Core.Entities;
using EduHome.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EduHome.Business.Implementations;
public class AuthService : IAuthService
{
    readonly UserManager<AppUser> _userManager;
    readonly IConfiguration _configuration;
    public AuthService(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto loginDto)
    {
        AppUser user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user is null)
            throw new AuthFailException("Username or password is wrong");

        bool check = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if(!check) throw new AuthFailException("Username or password is wrong");

        //Create JWT 

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
            expires: DateTime.UtcNow.AddSeconds(1),
            signingCredentials:signingCredentials);

        JwtSecurityTokenHandler tokenHandler = new();
        string token = tokenHandler.WriteToken(jwtToken);

        return new ()
        {
            Token = token,
            Username = user.UserName,
            Expires = jwtToken.ValidTo
        };
    }

    public async Task RegisterAsync(RegisterDto registerDto)
    {
        AppUser user = new()
        {
            Fullname = registerDto.Fullname,
            UserName = registerDto.Username,
            Email = registerDto.Email,
        };

        IdentityResult identityResult =  await _userManager.CreateAsync(user,registerDto.Password);

        if (!identityResult.Succeeded)
        {
            StringBuilder errors = new();
            foreach (var error in identityResult.Errors)
            {
                errors.Append(error.Description);
            }
            throw new UserCreateFailException(errors.ToString());
        }

        identityResult = await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

        if (!identityResult.Succeeded)
        {
            StringBuilder errors = new();
            foreach (var error in identityResult.Errors)
            {
                errors.Append(error.Description);
            }
            throw new RoleAddFailException(errors.ToString());
        }
    }
}

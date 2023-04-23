using EduHome.Business.DTOs.Auth;
using EduHome.Business.Exceptions;
using EduHome.Business.HelperServices.Interfaces;
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
    readonly ITokenHandler _tokenHandler;

    public AuthService(UserManager<AppUser> userManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto loginDto)
    {
        AppUser user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user is null)
            throw new AuthFailException("Username or password is wrong");

        bool check = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if(!check) throw new AuthFailException("Username or password is wrong");

        //Create JWT 
        return await _tokenHandler.GenerateTokenAsync(user,1);
       
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

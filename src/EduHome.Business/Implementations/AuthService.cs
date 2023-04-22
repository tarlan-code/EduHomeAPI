using EduHome.Business.DTOs.Auth;
using EduHome.Business.Exceptions;
using EduHome.Business.Interfaces;
using EduHome.Core.Entities;
using EduHome.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace EduHome.Business.Implementations;
public class AuthService : IAuthService
{
    readonly UserManager<AppUser> _userManager;

    public AuthService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
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
            errors.Append(identityResult.Errors.Select(x => x.Description + ","));
            throw new UserCreateFailException(errors.ToString());
        }

        identityResult = await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

        if (!identityResult.Succeeded)
        {
            StringBuilder errors = new();
            errors.Append(identityResult.Errors.Select(x => x.Description + ","));
            throw new RoleAddFailException(errors.ToString());
        }
    }
}

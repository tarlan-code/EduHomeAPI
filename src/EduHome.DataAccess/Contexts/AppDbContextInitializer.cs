using EduHome.Core.Entities;
using EduHome.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EduHome.DataAccess.Contexts;
public class AppDbContextInitializer
{
    readonly RoleManager<IdentityRole> _roleManager;
    readonly UserManager<AppUser> _userManager;
    readonly IConfiguration _configuration;
    readonly AppDbContext _context;
    public AppDbContextInitializer(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IConfiguration configuration, AppDbContext context)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
    }


    public async Task InitilaizeAsync()
    {
        await _context.Database.MigrateAsync();
    }
    public async Task RoleSeedAsync()
    {
        foreach (var role in Enum.GetValues(typeof(Roles)))
        {
            if (!await _roleManager.RoleExistsAsync(role.ToString()))
                await _roleManager.CreateAsync(new() { Name = role.ToString() }); 

        }
    }

    public async Task UserSeedAsync()
    {
        AppUser user = new()
        {
            UserName = _configuration["AdminSettings:Username"],
            Email = _configuration["AdminSettings:Email"]
        };
    
        await _userManager.CreateAsync(user, _configuration["AdminSettings:Password"]);
        await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
        
    }
}

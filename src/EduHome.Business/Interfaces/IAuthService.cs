using EduHome.Business.DTOs.Auth;

namespace EduHome.Business.Interfaces;
public interface IAuthService
{
    Task RegisterAsync(RegisterDto registerDto);
}

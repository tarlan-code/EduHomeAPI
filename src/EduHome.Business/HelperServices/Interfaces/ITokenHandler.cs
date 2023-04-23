using EduHome.Business.DTOs.Auth;
using EduHome.Core.Entities;

namespace EduHome.Business.HelperServices.Interfaces;
public interface ITokenHandler
{
    Task<TokenResponseDto> GenerateTokenAsync(AppUser user,int minute);
}

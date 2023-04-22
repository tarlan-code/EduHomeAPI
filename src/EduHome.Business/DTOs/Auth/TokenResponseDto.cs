namespace EduHome.Business.DTOs.Auth;
public class TokenResponseDto
{
    public string? Username { get; set; }
    public string? Token { get; set; }
    public DateTime Expires { get; set; }
}

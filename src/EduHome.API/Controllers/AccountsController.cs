using EduHome.Business.DTOs.Auth;
using EduHome.Business.Exceptions;
using EduHome.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EduHome.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    readonly IAuthService _authService;

    public AccountsController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("[action]")]

    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        try
        {
            await _authService.RegisterAsync(registerDto);

            return Ok("User successfully created");
        }
        catch(UserCreateFailException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(RoleAddFailException ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError,ex.Message);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
        
    }


    [HttpPost("[action]")]

    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        try
        {
            TokenResponseDto tokenResponse = await _authService.LoginAsync(loginDto);
            return Ok(tokenResponse);
        }
        catch (AuthFailException ex)
        {

            return BadRequest(ex.Message);
        }
        catch (Exception)
        {

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}

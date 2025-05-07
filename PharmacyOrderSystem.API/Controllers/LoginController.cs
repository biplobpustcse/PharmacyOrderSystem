using Microsoft.AspNetCore.Mvc;
using PharmacyOrderSystem.Application.DTOs;
using PharmacyOrderSystem.Application.Interfaces;

namespace PharmacyOrderSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public LoginController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDTO loginDto)
    {
        try
        {
            var token = await _authenticationService.AuthenticateAsync(
                loginDto.Username,
                loginDto.Password
            );
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { Message = "Invalid credentials" });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.DTO.Request;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Retorna errores de validación
        }

        var token = await _authService.Login(loginRequest.Email, loginRequest.Password);

        if (token == null)
        {
            return Unauthorized(new { Message = "Credenciales inválidas" });
        }

        return Ok(new { Token = token, Message = "Inicio de sesión exitoso" });
    }
}
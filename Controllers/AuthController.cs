using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] string usuario)
    {
        // Aquí podrías validar usuario y contraseña reales
        var token = TokenService.CrearToken(usuario);
        return Ok(new { token });
    }
}

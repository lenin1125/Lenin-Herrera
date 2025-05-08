using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;

namespace LeninHerrera.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValidarTokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ValidarTokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult ValidarToken([FromBody] TokenRequest request)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            try
            {
                tokenHandler.ValidateToken(request.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // sin tolerancia de tiempo

                }, out SecurityToken validatedToken);

                return Ok(new { valido = true, mensaje = "Token válido" });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { valido = false, mensaje = "Token inválido", detalle = ex.Message });
            }
        }
    }

    public class TokenRequest
    {
        public string Token { get; set; }
    }
}

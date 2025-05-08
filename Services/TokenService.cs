using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public static class TokenService
{
    public static string CrearToken(string usuario)
    {
        var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("clave-secreta-1234567890"));
        var credenciales = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario)
        };

        var token = new JwtSecurityToken(
            issuer: "miApp",
            audience: "miAppUsuarios",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credenciales
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

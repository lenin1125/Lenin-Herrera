using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Agrega la autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones =>
    {
        opciones.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "miApp",
            ValidAudience = "miAppUsuarios",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("clave-secreta-1234567890"))
        };
    });

builder.Services.AddControllers();
var app = builder.Build();

// Middleware
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

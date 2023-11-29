using DavidCardenasAPI.Context;
using DavidCardenasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DavidCardenasAPI.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly HalterofiliaContext _context;
        private readonly ILogger<HalterofiliaController> _logger;
        private readonly IConfiguration _configuration;
        bool logToDatabase = false;
        bool logToFile = false;
        public AuthenticationController(HalterofiliaContext context, ILogger<HalterofiliaController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            logToDatabase = _configuration.GetValue<bool>("Logging:DatabaseLogEnabled");
            logToFile = _configuration.GetValue<bool>("Logging:TextFileLogEnabled");
        }
        [HttpPost("GenerarToken")]
        public IActionResult GenerarToken()
        {
            try
            {
                var token = GenerarTokenJWT();

                return Ok(new { access_token = token });
            }
            catch (Exception ex)
            {
                if (logToFile)
                    _logger.LogError($"Error al GenerarToken: {ex.Message}");
                if (logToDatabase)
                {
                    _context.Logs.Add(new Log
                    {
                        Nivel = "Error",
                        Mensaje = $"Error al GenerarToken: {ex.Message}",
                        Fecha = DateTime.Now
                    });
                    _context.SaveChanges();
                }

                return StatusCode(500, new { Mensaje = "Error interno del servidor" });
            }
        }

        private string GenerarTokenJWT()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings").GetSection("Key").Value.ToString()));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "Halterofilia"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                string.Empty,
                string.Empty,
                claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}

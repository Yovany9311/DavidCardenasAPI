using DavidCardenasAPI.Business.Interfaces;
using DavidCardenasAPI.Domain.Models;
using DavidCardenasAPI.Security.Services;
using Microsoft.AspNetCore.Mvc;


namespace DavidCardenasAPI.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<HalterofiliaController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ILogService _logService;
        private readonly IAuthenticationJwtServices _authenticationJwtServices;
        bool logToDatabase = false;
        bool logToFile = false;
        public AuthenticationController(ILogger<HalterofiliaController> logger, IConfiguration configuration, ILogService logService, IAuthenticationJwtServices authenticationJwtServices)
        {
            _logger = logger;
            _configuration = configuration;
            _logService = logService;
            _authenticationJwtServices = authenticationJwtServices;
            logToDatabase = _configuration.GetValue<bool>("Logging:DatabaseLogEnabled");
            logToFile = _configuration.GetValue<bool>("Logging:TextFileLogEnabled");
        }
        [HttpPost("GenerarToken")]
        public async Task<IActionResult> GenerarToken()
        {
            try
            {
                var token = await _authenticationJwtServices.GetToken();

                return Ok(new { access_token = token });
            }
            catch (Exception ex)
            {
                if (logToFile)
                    _logger.LogError($"Error al GenerarToken: {ex.Message}");
                if (logToDatabase)
                {
                    LogApi log = new LogApi()
                    {
                        Nivel = "Error",
                        Mensaje = $"Error al GenerarToken: {ex.Message}",
                        Fecha = DateTime.Now
                    };
                    await _logService.Save(log);
                }

                return StatusCode(500, new { Mensaje = "Error interno del servidor" });
            }
        }
    }
}

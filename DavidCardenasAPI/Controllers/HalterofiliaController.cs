using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DavidCardenasAPI.Business.Interfaces;
using DavidCardenasAPI.Domain.DTOS;
using DavidCardenasAPI.Domain.Models;
using Serilog;
using DavidCardenasAPI.Data.Context;
using DavidCardenasAPI.Business.Services;

namespace DavidCardenasAPI.Controllers
{
    [Authorize]
    [Route("api/halterofilia")]
    [ApiController]
    public class HalterofiliaController : ControllerBase
    {
        private readonly HalterofiliaContext _context;
        private readonly ILogger<HalterofiliaController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDeportistasService _deportistasService;
        private readonly ILogService _logService;
        private readonly IResultadoService _resultadoService;
        bool logToDatabase = false;
        bool logToFile = false;
        public HalterofiliaController(IDeportistasService deportistasService, ILogService logService, IResultadoService resultadosService, HalterofiliaContext context, ILogger<HalterofiliaController> logger,
            IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _logService = logService;
            _resultadoService = resultadosService;
            logToDatabase = _configuration.GetValue<bool>("Logging:DatabaseLogEnabled");
            logToFile = _configuration.GetValue<bool>("Logging:TextFileLogEnabled");
            _deportistasService = deportistasService;

        }

        [HttpPost("AgregarDeportista")]
        public async Task<ActionResult> AgregarDeportista(DTODeportista dtoDeportista)
        {
            try
            {
                await _deportistasService.Save(dtoDeportista);

                if (logToFile)
                    _logger.LogInformation($"Deportista agregado: {dtoDeportista.Nombre}");

                if (logToDatabase)
                {
                    LogApi log = new LogApi()
                    {
                        Nivel = "Información",
                        Mensaje = $"Resultado agregado para el deportista {dtoDeportista.Nombre}",
                        Fecha = DateTime.Now
                    };
                    await _logService.Save(log);
                }

                return Ok(new { Mensaje = "Deportista agregado correctamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al agregar deportista: {ex.Message}");
                if (logToDatabase)
                {
                    await _logService.Save(new LogApi
                    {
                        Nivel = "Error",
                        Mensaje = $"Error al agregar resultado: {ex.Message}",
                        Fecha = DateTime.Now
                    });
                }
                return StatusCode(500, new { Mensaje = "Error interno del servidor" });
            }
        }

        [HttpPut("ActualizarDeportista")]
        public async Task<ActionResult> ActualizarDeportista(DTODeportista dtoDeportista)
        {
            try
            {
                await _deportistasService.Update(dtoDeportista);

                if (logToFile)
                    _logger.LogInformation($"Deportista Actualizado: {dtoDeportista.Nombre}");

                if (logToDatabase)
                {
                    LogApi log = new LogApi()
                    {
                        Nivel = "Información",
                        Mensaje = $"Resultado Actualizado para el deportista {dtoDeportista.Nombre}",
                        Fecha = DateTime.Now
                    };
                    await _logService.Save(log);
                }

                return Ok(new { Mensaje = "Deportista Actualizado correctamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al Actualizar deportista: {ex.Message}");
                if (logToDatabase)
                {
                    await _logService.Save(new LogApi
                    {
                        Nivel = "Error",
                        Mensaje = $"Error al Actualizar resultado: {ex.Message}",
                        Fecha = DateTime.Now
                    });
                }
                return StatusCode(500, new { Mensaje = "Error interno del servidor" });
            }
        }

        [HttpDelete("EliminarDeportista")]
        public async Task<IActionResult> EliminarDeportista(int id)
        {
            try
            {
                return Ok(await _deportistasService.Delete(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al Eliminar deportista: {ex.Message}");
                if (logToDatabase)
                {
                    await _logService.Save(new LogApi
                    {
                        Nivel = "Error",
                        Mensaje = $"Error al Eliminar resultado: {ex.Message}",
                        Fecha = DateTime.Now
                    });
                }
                return StatusCode(500, new { Mensaje = "Error interno del servidor" });
            }
        }


        [HttpPost("AgregarResultado")]
        public async Task<ActionResult> AgregarResultado(DTOResultado dtoResultado)
        {
            try
            {
                Resultado resultado = new Resultado()
                {
                    Arranque = dtoResultado.Arranque,
                    Envion = dtoResultado.Envion,
                    DeportistaId = dtoResultado.DeportistaId,
                    TotalPeso = dtoResultado.TotalPeso
                };

                await _resultadoService.Save(resultado);
                if (logToFile)
                    _logger.LogInformation($"Resultado agregado para el deportista {resultado.DeportistaId}");

                if (logToDatabase)
                {
                    LogApi log = new LogApi()
                    {
                        Nivel = "Información",
                        Mensaje = $"Resultado agregado para el deportista {resultado.DeportistaId}",
                        Fecha = DateTime.Now
                    };
                    await _logService.Save(log);
                }

                return Ok(new { Mensaje = "Resultado agregado correctamente" });
            }
            catch (Exception ex)
            {
                if (logToFile)
                    _logger.LogError($"Error al agregar resultado: {ex.Message}");
                if (logToDatabase)
                {
                    LogApi log = new LogApi()
                    {
                        Nivel = "Error",
                        Mensaje = $"Error al agregar resultado: {ex.Message}",
                        Fecha = DateTime.Now
                    };
                    await _logService.Save(log);
                }

                return StatusCode(500, new { Mensaje = "Error interno del servidor" });
            }
        }
        [HttpGet("ObtenerTablaClasificacion")]
        public async Task<ActionResult<IEnumerable<object>>> ObtenerTablaClasificacion()
        {
            try
            {
                return Ok(await _resultadoService.GetTablaClasificacion());
            }
            catch (Exception ex)
            {
                if (logToFile)
                    _logger.LogError($"Error al consultar  ObtenerTablaClasificacion: {ex.Message}");
                if (logToDatabase)
                {
                    LogApi log = new LogApi()
                    {
                        Nivel = "Error",
                        Mensaje = $"Error al consultar  ObtenerTablaClasificacion: {ex.Message}",
                        Fecha = DateTime.Now
                    };
                    await _logService.Save(log);
                }

                return StatusCode(500, new { Mensaje = "Error interno del servidor" });
            }
        }

        [HttpGet("ObtenerIntentosDeportista")]
        public async Task<ActionResult<IEnumerable<object>>> ObtenerIntentosDeportista()
        {
            try
            {
                return Ok(await _resultadoService.GetIntentosDeportits());
            }
            catch (Exception ex)
            {
                if (logToFile)
                    _logger.LogError($"Error al consultar  ObtenerTablaClasificacion: {ex.Message}");
                if (logToDatabase)
                {
                    LogApi log = new LogApi()
                    {
                        Nivel = "Error",
                        Mensaje = $"Error al consultar  ObtenerTablaClasificacion: {ex.Message}",
                        Fecha = DateTime.Now
                    };
                    await _logService.Save(log);
                }

                return StatusCode(500, new { Mensaje = "Error interno del servidor" });
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using DavidCardenasAPI.Context;
using DavidCardenasAPI.Models;
using Microsoft.Extensions.Configuration;
using DavidCardenasAPI.DTOS;

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
        bool logToDatabase = false;
        bool logToFile = false;
        public HalterofiliaController(HalterofiliaContext context, ILogger<HalterofiliaController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            logToDatabase = _configuration.GetValue<bool>("Logging:DatabaseLogEnabled");
            logToFile = _configuration.GetValue<bool>("Logging:TextFileLogEnabled");

        }

        [HttpPost("AgregarDeportista")]
        public ActionResult AgregarDeportista(DTODeportista dtoDeportista)
        {
            try
            {
                Deportista deportista = new Deportista()
                {
                    Nombre = dtoDeportista.Nombre,
                    Pais = dtoDeportista.Pais,
                };
                _context.Deportistas.Add(deportista);
                _context.SaveChanges();

                if (logToFile)
                    _logger.LogInformation($"Deportista agregado: {deportista.Nombre}");

                if (logToDatabase)
                {
                    _context.Logs.Add(new Log
                    {
                        Nivel = "Información",
                        Mensaje = $"Resultado agregado para el deportista {deportista.Nombre}",
                        Fecha = DateTime.Now
                    });
                    _context.SaveChanges();
                }

                return Ok(new { Mensaje = "Deportista agregado correctamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al agregar deportista: {ex.Message}");
                if (logToDatabase)
                {
                    _context.Logs.Add(new Log
                    {
                        Nivel = "Error",
                        Mensaje = $"Error al agregar resultado: {ex.Message}",
                        Fecha = DateTime.Now
                    });
                    _context.SaveChanges();
                }
                return StatusCode(500, new { Mensaje = "Error interno del servidor" });
            }
        }

        [HttpPost("AgregarResultado")]
        public ActionResult AgregarResultado(DTOResultado dtoResultado)
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
                _context.Resultados.Add(resultado);
                _context.SaveChanges();
                if (logToFile)
                    _logger.LogInformation($"Resultado agregado para el deportista {resultado.DeportistaId}");

                if (logToDatabase)
                {
                    _context.Logs.Add(new Log
                    {
                        Nivel = "Información",
                        Mensaje = $"Resultado agregado para el deportista {resultado.DeportistaId}",
                        Fecha = DateTime.Now
                    });
                    _context.SaveChanges();
                }

                return Ok(new { Mensaje = "Resultado agregado correctamente" });
            }
            catch (Exception ex)
            {
                if (logToFile)
                    _logger.LogError($"Error al agregar resultado: {ex.Message}");
                if (logToDatabase)
                {
                    _context.Logs.Add(new Log
                    {
                        Nivel = "Error",
                        Mensaje = $"Error al agregar resultado: {ex.Message}",
                        Fecha = DateTime.Now
                    });
                    _context.SaveChanges();
                }

                return StatusCode(500, new { Mensaje = "Error interno del servidor" });
            }
        }
        [HttpGet("ObtenerTablaClasificacion")]
        public ActionResult<IEnumerable<object>> ObtenerTablaClasificacion()
        {
            try
            {
                var tabla = _context.Resultados
                         .GroupBy(r => new { r.Deportista.Pais, r.Deportista.Nombre })
                         .Select(g => new
                         {
                             Pais = g.Key.Pais,
                             Nombre = g.Key.Nombre,
                             Arranque = g.Max(r => r.Arranque),
                             Envion = g.Max(r => r.Envion),
                             TotalPeso = g.Max(r => r.TotalPeso)
                         })
                         .OrderByDescending(d => d.TotalPeso)
                         .ToList();

                return Ok(tabla);
            }
            catch (Exception ex)
            {
                if (logToFile)
                    _logger.LogError($"Error al consultar  ObtenerTablaClasificacion: {ex.Message}");
                if (logToDatabase)
                {
                    _context.Logs.Add(new Log
                    {
                        Nivel = "Error",
                        Mensaje = $"Error al consultar  ObtenerTablaClasificacion: {ex.Message}",
                        Fecha = DateTime.Now
                    });
                    _context.SaveChanges();
                }

                return StatusCode(500, new { Mensaje = "Error interno del servidor" });
            }
        }

        [HttpGet("ObtenerIntentosDeportista")]
        public ActionResult<IEnumerable<object>> ObtenerIntentosDeportista()
        {
            try
            {
                var intentos = _context.Resultados
                .GroupBy(r => r.Deportista.Nombre)
                .Select(g => new
                {
                    Nombre = g.Key,
                    IntentosArranque = g.Count(r => r.Arranque > 0),
                    IntentosEnvion = g.Count(r => r.Envion > 0)
                })
                .ToList();

                return Ok(intentos);
            }
            catch (Exception ex)
            {
                if (logToFile)
                    _logger.LogError($"Error al consultar  ObtenerTablaClasificacion: {ex.Message}");
                if (logToDatabase)
                {
                    _context.Logs.Add(new Log
                    {
                        Nivel = "Error",
                        Mensaje = $"Error al consultar  ObtenerTablaClasificacion: {ex.Message}",
                        Fecha = DateTime.Now
                    });
                    _context.SaveChanges();
                }

                return StatusCode(500, new { Mensaje = "Error interno del servidor" });
            }
        }
    }
}
using DavidCardenasAPI.Business.Interfaces;
using DavidCardenasAPI.Data.Interfaces;
using DavidCardenasAPI.Domain.DTOS;
using DavidCardenasAPI.Domain.Models;

namespace DavidCardenasAPI.Business.Services
{
    public class ResultadoService : IResultadoService
    {
        private readonly IRepository<Resultado> _resultadoRepository;
        private readonly IRepository<Deportista> _deportistaRepositry;
        public ResultadoService(IRepository<Resultado> resultadoRepository, IRepository<Deportista> deportistaRepositry)
        {
            _resultadoRepository = resultadoRepository;
            _deportistaRepositry = deportistaRepositry;
        }

        public async Task<IEnumerable<DTOClasificacion>> GetTablaClasificacion()
        {
            IEnumerable<Resultado> resultClasificacion = await _resultadoRepository.GetAll();

            foreach (var item in resultClasificacion)
            {
                item.Deportista = await _deportistaRepositry.GetById(item.DeportistaId);
            }
            var tablaClasificacion = resultClasificacion.GroupBy(r => new { r.Deportista.Pais, r.Deportista.Nombre })
                                   .Select(g => new DTOClasificacion
                                   {
                                       Pais = g.Key.Pais,
                                       Nombre = g.Key.Nombre,
                                       Arranque = g.Max(r => r.Arranque),
                                       Envion = g.Max(r => r.Envion),
                                       TotalPeso = g.Max(r => r.TotalPeso)
                                   })
                                   .OrderByDescending(d => d.TotalPeso)
                                   .ToList();
            return tablaClasificacion;
        }

        public async Task<IEnumerable<DTOIntentosDeportits>> GetIntentosDeportits()
        {
            IEnumerable<Resultado> resultIntetos = await _resultadoRepository.GetAll();
            foreach (var item in resultIntetos)
            {
                item.Deportista = await _deportistaRepositry.GetById(item.DeportistaId);
            }
            var tablaClasificacion = resultIntetos.GroupBy(r => r.Deportista.Nombre)
                                     .Select(g => new DTOIntentosDeportits
                                     {
                                         Nombre = g.Key,
                                         IntentosArranque = g.Count(r => r.Arranque > 0),
                                         IntentosEnvion = g.Count(r => r.Envion > 0)
                                     })
                                     .ToList();
            return tablaClasificacion;
        }

        public async Task<Resultado> Save(Resultado entity)
        {
            return await _resultadoRepository.Save(entity);
        }
    }
}

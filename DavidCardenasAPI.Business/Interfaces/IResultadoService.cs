using DavidCardenasAPI.Domain.DTOS;
using DavidCardenasAPI.Domain.Models;

namespace DavidCardenasAPI.Business.Interfaces
{
    public interface IResultadoService
    {
        Task<IEnumerable<DTOClasificacion>> GetTablaClasificacion();
        Task<IEnumerable<DTOIntentosDeportits>> GetIntentosDeportits();
        Task<Resultado> Save(Resultado entity);
    }
}

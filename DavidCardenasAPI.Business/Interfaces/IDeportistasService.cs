using DavidCardenasAPI.Domain.DTOS;
using DavidCardenasAPI.Domain.Models;

namespace DavidCardenasAPI.Business.Interfaces
{
    public interface IDeportistasService
    {
        Task<IEnumerable<Deportista>> GetAll();
        Task<Deportista> GetById(int id);
        Task<Deportista> Save(DTODeportista entity);
        Task<Deportista> Delete(int id);
        Task<Deportista> Update(DTODeportista entity);
    }
}

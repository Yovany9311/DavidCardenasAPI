using DavidCardenasAPI.Business.Interfaces;
using DavidCardenasAPI.Data.Interfaces;
using DavidCardenasAPI.Domain.DTOS;
using DavidCardenasAPI.Domain.Models;

namespace WSServicioPichincha.Business.Services
{
    public class DeportistasService : IDeportistasService
    {
        private readonly IRepository<Deportista> deportistaRepository;
        public DeportistasService(IRepository<Deportista> deportistaRepository)
        {
            this.deportistaRepository = deportistaRepository;
        }
        public async Task<Deportista> Delete(int id)
        {
            Deportista deportistaDb = await deportistaRepository.GetById(id);
            if (deportistaDb != null)
            {
                return await deportistaRepository.Delete(id);
            }
            else
            {
                throw new Exception("El deportista que intenta actualizar no existe");
            }
        }

        public async Task<IEnumerable<Deportista>> GetAll()
        {
            return await deportistaRepository.GetAll();
        }

        public async Task<Deportista> GetById(int id)
        {
            return await deportistaRepository.GetById(id);
        }

        public async Task<Deportista> Save(DTODeportista dtoDeportista)
        {
            Deportista entity = new Deportista()
            {
                Nombre = dtoDeportista.Nombre,
                Pais = dtoDeportista.Pais,
            };
            return await deportistaRepository.Save(entity);
        }

        public async Task<Deportista> Update(DTODeportista dtoDeportista)
        {
            Deportista deportistaDb = await deportistaRepository.GetById(dtoDeportista.Id);
            if (deportistaDb != null)
            {
                Deportista entity = new Deportista()
                {
                    Id = deportistaDb.Id,
                    Nombre = dtoDeportista.Nombre,
                    Pais = dtoDeportista.Pais,
                };
                return await deportistaRepository.Update(entity);
            }
            else
            {
                throw new Exception("El deportista que intenta actualizar no existe");
            }
        }
    }
}

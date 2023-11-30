using Microsoft.EntityFrameworkCore;
using DavidCardenasAPI.Data.Context;
using DavidCardenasAPI.Data.Interfaces;
using System.Linq;
namespace WSServicioPichincha.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly HalterofiliaContext  halterofiliaContext;
        public Repository(HalterofiliaContext halterofiliaContext)
        {
            this.halterofiliaContext = halterofiliaContext;
        }
        protected DbSet<T> EntitySet
        {
            get
            {
                return halterofiliaContext.Set<T>();
            }
        }
        public async Task<T> Delete(int id)
        {
            try
            {
                T entity = await EntitySet.FindAsync(id);
                EntitySet.Remove(entity);
                halterofiliaContext.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Se presento un error al intertar eliminar la información, Message: {ex.Message}");
            }
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await EntitySet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Se presento un error al intertar obtener toda la información, Message: {ex.Message}");
            }
        }

        public async Task<T> GetById(int id)
        {
            try
            {
                return await EntitySet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Se presento un error al intertar obtener la información por Id, Message: {ex.Message}");
            }
        }

        public async Task<T> Save(T entity)
        {
            try
            {
                EntitySet.Add(entity);
                await halterofiliaContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Se presento un error al intertar guardar la información, Message: {ex.Message}");
            }
        }

        public async Task<T> Update(T entity)
        {
            try
            {
                halterofiliaContext.Entry(entity).State = EntityState.Modified;
                await halterofiliaContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Se presento un error al intertar actualizar la información, Message: {ex.Message}");
            }
        }
    }
}

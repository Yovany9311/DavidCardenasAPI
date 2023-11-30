
namespace DavidCardenasAPI.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Save(T entity);
        Task<T> Delete(int id);
        Task<T> Update(T entity);
    }
}

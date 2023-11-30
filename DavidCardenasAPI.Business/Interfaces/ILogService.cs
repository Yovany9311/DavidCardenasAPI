using DavidCardenasAPI.Domain.Models;

namespace DavidCardenasAPI.Business.Interfaces
{
    public interface ILogService
    {
        Task<IEnumerable<LogApi>> GetAll();
        Task<LogApi> GetById(int id);
        Task<LogApi> Save(LogApi entity);
        Task<LogApi> Delete(int id);
        Task<LogApi> Update(LogApi entity);
    }
}

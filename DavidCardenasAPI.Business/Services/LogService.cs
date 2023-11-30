using DavidCardenasAPI.Business.Interfaces;
using DavidCardenasAPI.Data.Interfaces;
using DavidCardenasAPI.Domain.Models;

namespace DavidCardenasAPI.Business.Services
{
    public class LogService : ILogService
    {
        private readonly IRepository<LogApi> logRepository;
        public LogService(IRepository<LogApi> logRepository)
        {
            this.logRepository = logRepository;
        }
        public async Task<LogApi> Delete(int id)
        {
            return await logRepository.Delete(id);
        }

        public async Task<IEnumerable<LogApi>> GetAll()
        {
            return await logRepository.GetAll();
        }

        public async Task<LogApi> GetById(int id)
        {
            return await logRepository.GetById(id);
        }

        public async Task<LogApi> Save(LogApi entity)
        {
            return await logRepository.Save(entity);
        }

        public async Task<LogApi> Update(LogApi entity)
        {
            return await logRepository.Update(entity);
        }
    }
}

using DataAccessLayer.IRepositories;
using DomainLayer.Dtos;

namespace BusinessLayer.Services.LogService
{
    public class LogsService
    {
        private readonly ILogsRepository _logsRepository;
        public LogsService(ILogsRepository logsRepository)
        {
            _logsRepository = logsRepository;
        }
        public async Task<LogsOutputDto> GetAllLogsAsync(int pageNumber, int pageSize)
        {
            return await _logsRepository.GetAllLogs(pageNumber, pageSize);
        }
    }
}

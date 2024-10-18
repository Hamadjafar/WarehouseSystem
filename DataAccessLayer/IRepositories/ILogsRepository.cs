
using DomainLayer.Dtos;

namespace DataAccessLayer.IRepositories
{
    public interface ILogsRepository
    {
        Task<LogsOutputDto> GetAllLogs(int pageNumber, int pageSize);
    }
}

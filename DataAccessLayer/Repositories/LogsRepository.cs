using AutoMapper;
using DataAccessLayer.IRepositories;
using DomainLayer.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class LogsRepository : ILogsRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LogsRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<LogsOutputDto> GetAllLogs(int pageNumber, int pageSize)
        {
            var totalItems = await _context.Logs.CountAsync();
            var logs = await _context.Logs
           .OrderByDescending(log => log.Timestamp) 
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .AsNoTracking()
           .ToListAsync();
            var logsMapper = _mapper.Map<List<LogsDto>>(logs);

            return new LogsOutputDto
            {
                LogsDtos = logsMapper,
                TotalItems = totalItems
            };
        }
    }
}

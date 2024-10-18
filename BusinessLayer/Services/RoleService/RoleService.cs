using AutoMapper;
using DataAccessLayer.IRepositories;
using DomainLayer.Dtos;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services.RoleService
{
    public class RoleService 
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleService> _logger;


        public RoleService(IRoleRepository roleRepository, IMapper mapper, ILogger<RoleService> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<RoleDto>> GetAllRolesAsync()
        {
            _logger.LogInformation("Fetching all roles from the database");

            var roles = await _roleRepository.GetAllRolesAsync();
            _logger.LogInformation($"Successfully retrieved {roles.Count} roles");
            return _mapper.Map<List<RoleDto>>(roles);
        }
    }
}

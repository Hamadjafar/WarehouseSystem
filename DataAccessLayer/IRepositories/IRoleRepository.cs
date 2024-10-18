using DomainLayer.Entities;
namespace DataAccessLayer.IRepositories
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllRolesAsync();
    }
}

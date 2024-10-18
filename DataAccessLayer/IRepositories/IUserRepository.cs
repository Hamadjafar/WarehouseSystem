using DomainLayer.Dtos;
using DomainLayer.Entities;


namespace DataAccessLayer.IRepositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
        Task UpdateUser(User user);
        Task<UsersOutputDto> GetAllUsers(int pageNumber, int pageSize);
        Task<bool> IsEmailExists(string email);    
    }
}

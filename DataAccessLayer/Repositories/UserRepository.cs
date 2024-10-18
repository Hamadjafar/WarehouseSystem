using AutoMapper;
using DataAccessLayer.IRepositories;
using DomainLayer.Dtos;
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.AsNoTracking().Include(x => x.Role).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FirstAsync(u => u.Id == id);
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UsersOutputDto> GetAllUsers(int pageNumber, int pageSize)
        {
            var totalItems = await _context.Warehouses.CountAsync();


            var users = await _context.Users.Include(u => u.Role).Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .AsNoTracking()
                        .Where(u => u.IsActive)
                        .ToListAsync();


            var userMapperDto = _mapper.Map<List<GetAllUsersOutput>>(users);
            return new UsersOutputDto
            {
                UserDto = userMapperDto,
                TotalItems = totalItems,
            };
        }

        public async Task<bool> IsEmailExists(string email)
        {
            return await _context.Users.AsNoTracking().AnyAsync(u => u.Email == email);
        }
    }
}

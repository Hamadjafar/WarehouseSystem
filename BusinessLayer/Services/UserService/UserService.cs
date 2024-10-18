using AutoMapper;
using DataAccessLayer.IRepositories;
using DomainLayer.Dtos;
using DomainLayer.Entities;
using DomainLayer.Utilities;
using Infrastructure.Shared;
using Microsoft.Extensions.Logging;


namespace BusinessLayer.Services.UserService
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly PasswordHasher _passwordHasher;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, IMapper mapper, PasswordHasher passwordHasher, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task CreateOrUpdateUserAsync(UserDto userDto)
        {
            Guard.AssertArgumentNotNull(userDto, nameof(userDto));
            Guard.AssertArgumentNotLessThanOrEqualToZero(userDto.RoleId, nameof(userDto.RoleId));

            if (userDto.Id > 0)
            {
                _logger.LogInformation($"Updating user with ID: {userDto.Id}");
                var user = await GetUserEntityByIdAsync(userDto.Id);

                _mapper.Map(userDto, user);
                await _userRepository.UpdateUser(user);
                _logger.LogInformation($"User with ID: {userDto.Id} updated successfully.");
            }
            else
            {
                _logger.LogInformation($"Attempting to create a user with email: {userDto.Email}");
                if (await _userRepository.IsEmailExists(userDto.Email))
                    throw new InvalidOperationException("The email address is already exist. Please use a different email.");

                var user = _mapper.Map<User>(userDto);
                var generatedPassword = GenerateRandomPassword(12);
                user.PasswordHash = _passwordHasher.HashPassword(generatedPassword);

                await _userRepository.CreateUserAsync(user);
                _logger.LogInformation($"User created successfully with email: {userDto.Email}");
            }
           

        }

        public async Task DeactivateUserAsync(int userId)
        {
            var user = await GetUserEntityByIdAsync(userId);
            Guard.AssertArgumentNotNull(user, nameof(user));
            if (!user.IsActive)
            {
                throw new InvalidOperationException("This account is already deactivated.");
            }
            user.IsActive = false;
            await _userRepository.UpdateUser(user);
            _logger.LogInformation($"User with ID: {userId} has been deactivated.");
        }

        public async Task<UsersOutputDto> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            _logger.LogInformation("Retrieving all users.");

            return await _userRepository.GetAllUsers(pageNumber, pageSize);
            
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            _logger.LogInformation($"Retrieving user by ID: {userId}");

            var user = await GetUserEntityByIdAsync(userId);

            return _mapper.Map<UserDto>(user);
        }

        public async Task ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            _logger.LogInformation($"Attempting to change password for user with ID: {changePasswordDto.UserId}");

            var user = await _userRepository.GetUserById(changePasswordDto.UserId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.PasswordHash = _passwordHasher.HashPassword(changePasswordDto.NewPassword);
            await _userRepository.UpdateUser(user);
            _logger.LogInformation($"Password changed successfully for user with ID: {changePasswordDto.UserId}");

        }

        private async Task<User> GetUserEntityByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {userId} not found.");
            }
            _logger.LogInformation($"Retrieved user with ID: {userId}");
            return user;
        }


        private string GenerateRandomPassword(int length)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            Random random = new Random();
            return new string(Enumerable.Repeat(validChars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

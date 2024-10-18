using BusinessLayer.Services.UserService;
using DataAccessLayer.IRepositories;
using DataAccessLayer.Repositories;
using DomainLayer.Dtos;
using DomainLayer.Utilities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;


namespace Test.IntegrationTest
{
    public class UserServiceTest : BaseServiceTest
    {
        private readonly UserService _service;
        private readonly IUserRepository _repository;
        private readonly Mock<ILogger<UserService>> _loggerMock;
        public UserServiceTest()
        {
            _repository = new UserRepository(_dbContext, _mapper);

            _loggerMock = new Mock<ILogger<UserService>>();

            _service = new UserService(_repository, _mapper, new PasswordHasher(), _loggerMock.Object);
        }

        [Fact]
        public async Task Should_Create_User_Successfully()
        {
            // Arrange
            var userDto = new UserDto
            {
                Email = "testuser@example.com",
                RoleId = 1,
                Name = "hamad",
                IsActive = true,
            };

            // Act
            await _service.CreateOrUpdateUserAsync(userDto);

            // Assert
            var createdUser = await _repository.GetUserByEmail(userDto.Email); // Assume GetUserByEmail is defined in your repository
            createdUser.Should().NotBeNull();
            createdUser.Email.Should().Be(userDto.Email);
        }

        [Fact]
        public async Task Should_Update_User_Successfully()
        {
            // Arrange
            var userDto = new UserDto
            {
                Email = "testuser@example.com",
                RoleId = 1,
                Name = "hamad",
                IsActive = true,
            };

            // Act
            await _service.CreateOrUpdateUserAsync(userDto);
            var createdUser = await _repository.GetUserByEmail(userDto.Email);


            var updateUser = new UserDto
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                RoleId = createdUser.RoleId,
                Name = "ahmad",
                IsActive = true,
            };
            await _service.CreateOrUpdateUserAsync(updateUser);
            // Assert

            var result = await _repository.GetUserByEmail(updateUser.Email);
            result.Should().NotBeNull();
            result.Email.Should().Be(updateUser.Email);
            result.Name.Should().Be(updateUser.Name);
        }

        [Fact]
        public async Task Should_Deactivate_User_Successfully()
        {
            // Arrange
            var userDto = new UserDto
            {
                Email = "activeuser@example.com",
                RoleId = 1,
                Name = "hamad",
                IsActive = true,
            };

            await _service.CreateOrUpdateUserAsync(userDto);
            var user = await _repository.GetUserByEmail(userDto.Email);

            // Act
            await _service.DeactivateUserAsync(user.Id);

            // Assert
            var deactivatedUser = await _repository.GetUserById(user.Id);
            deactivatedUser.Should().NotBeNull();
            deactivatedUser.IsActive.Should().BeFalse();
        }

        [Fact]
        public async Task Should_Update_User_Password_Successfully()
        {
            // Arrange
            var userDto = new UserDto
            {
                Email = "passworduser@example.com",
                RoleId = 1,
                Name = "hamad",
                IsActive = true,
            };

            await _service.CreateOrUpdateUserAsync(userDto);
            var user = await _repository.GetUserByEmail(userDto.Email);

            var changePasswordDto = new ChangePasswordDto
            {
                UserId = user.Id,
                NewPassword = "NewStrongPassword123!",
            };

            // Act
            await _service.ChangePasswordAsync(changePasswordDto);

            // Assert
            var updatedUser = await _repository.GetUserById(user.Id);
            updatedUser.PasswordHash.Should().NotBe(user.PasswordHash); 
        }

        [Fact]
        public async Task Should_Return_All_Users_Successfully()
        {
            // Arrange
            var userDto1 = new UserDto { Email = "user1@example.com", RoleId = 1, Name = "hamad", IsActive = true };
            var userDto2 = new UserDto { Email = "user2@example.com", RoleId = 1, Name = "adel", IsActive = true };

            await _service.CreateOrUpdateUserAsync(userDto1);
            await _service.CreateOrUpdateUserAsync(userDto2);

            // Act
            var usersOutputDto = await _service.GetAllUsersAsync(1, 10);

            // Assert
            usersOutputDto.Should().NotBeNull();
            usersOutputDto.UserDto.Should().HaveCount(3); //3 becuse the default user admin
        }

        [Fact]
        public async Task Should_ThrowException_When_DuplicateEmail()
        {
            // Arrange
            var userDto = new UserDto
            {
                Email = "testuser@example.com",
                RoleId = 1,
                Name = "hamad",
                IsActive = true,
            };

            var userDto2 = new UserDto
            {
                Email = "testuser@example.com",
                RoleId = 3,
                Name = "adel",
                IsActive = true,
            };

            await _service.CreateOrUpdateUserAsync(userDto);

            
            // Act
            Func<Task> act = async () => await _service.CreateOrUpdateUserAsync(userDto2);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("The email address is already exist. Please use a different email.");
        }

    }
}

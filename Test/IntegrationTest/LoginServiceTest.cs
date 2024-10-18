using BusinessLayer.Services.AuthService;
using BusinessLayer.Services.UserService;
using DataAccessLayer.IRepositories;
using DataAccessLayer.Repositories;
using DomainLayer.Entities;
using DomainLayer.Utilities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.IntegrationTest
{

    public class LoginServiceTest : BaseServiceTest
    {
        private readonly AuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<ILogger<AuthService>> _loggerMock;
        private readonly PasswordHasher _passwordHasher;
        public LoginServiceTest()
        {
            _userRepository = new UserRepository(_dbContext, _mapper);
            _loggerMock = new Mock<ILogger<AuthService>>();
            _configurationMock = new Mock<IConfiguration>();
            _passwordHasher = new PasswordHasher();

            _authService = new AuthService(_userRepository, _configurationMock.Object, _passwordHasher, _loggerMock.Object);
        }

        [Fact]
        public async Task Should_Return_Token_When_Credentials_Are_Valid()
        {
            // Arrange
            var email = "validuser@example.com";
            var password = "ValidPassword123!";

            // Pre-create a user with valid credentials in the test database
            var user = new User
            {
                Email = email,
                Name = "hamad",
                PasswordHash = _passwordHasher.HashPassword(password), // Hash the password
                IsActive = true,
                Role = new Role { Name = "Admin" }
            };

            await _userRepository.CreateUserAsync(user); // Create user in the database
            _configurationMock.Setup(c => c["JWT:Key"]).Returns("3YHVtCsSGvitUK3sg0pmUoSTYw1VKQllF4Mt8hNTiPY=");
            _configurationMock.Setup(c => c["JWT:Issuer"]).Returns("SecureApi");
            _configurationMock.Setup(c => c["JWT:Audience"]).Returns("SecureApiUser");
            // Act
            var authModel = await _authService.LoginAsync(email, password);

            // Assert
            authModel.Should().NotBeNull();
            authModel.Token.Should().NotBeNullOrWhiteSpace();
            authModel.Role.Should().Be("Admin");
            authModel.Message.Should().Be("Login successful");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_User_Is_Inactive()
        {
            // Arrange
            var email = "inactiveuser@example.com";
            var password = "ValidPassword123!";

            var user = new User
            {
                Email = email,
                Name = "hamad",
                PasswordHash = _passwordHasher.HashPassword(password),
                IsActive = false,
                Role = new Role { Name = "Admin" }
            };

            await _userRepository.CreateUserAsync(user);

            // Act
            Func<Task> act = async () => await _authService.LoginAsync(email, password);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Your account is disabled. Please contact support for assistance.");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Password_Is_Incorrect()
        {
            // Arrange
            var email = "user@example.com";
            var password = "CorrectPassword!";
            var wrongPassword = "WrongPassword!";

            var user = new User
            {
                Email = email,
                Name = "hamad",
                PasswordHash = _passwordHasher.HashPassword(password),
                IsActive = true,
                Role = new Role { Name = "Admin" }
            };

            await _userRepository.CreateUserAsync(user);

            // Act
            Func<Task> act = async () => await _authService.LoginAsync(email, wrongPassword);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Invalid login attempt. Password is incorrect.");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Email_Does_Not_Exist()
        {
            // Arrange
            var email = "nonexistent@example.com";
            var password = "AnyPassword!";

            // Act
            Func<Task> act = async () => await _authService.LoginAsync(email, password);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Invalid login attempt. Password is incorrect.");
        }
    }
}

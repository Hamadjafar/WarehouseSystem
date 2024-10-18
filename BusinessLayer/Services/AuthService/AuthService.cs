using DataAccessLayer.IRepositories;
using DomainLayer.Dtos;
using DomainLayer.Entities;
using DomainLayer.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Services.AuthService
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthService> _logger;
        public AuthService(IUserRepository userRepository ,IConfiguration configuration, PasswordHasher passwordHasher, ILogger<AuthService> logger)
        {
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;   
            _logger = logger;
        }

        public async Task<AuthModel> LoginAsync(string email, string password)
        {

            var user = await _userRepository.GetUserByEmail(email);
            if (user != null)
            {
                _logger.LogInformation($"Login attempt for username: {user.Name}");


                if (!user.IsActive)
                    throw new Exception("Your account is disabled. Please contact support for assistance.");


                if (!_passwordHasher.VerifyPassword(user.PasswordHash, password))
                    throw new Exception("Invalid login attempt. Password is incorrect.");



                var token = GenerateJwtToken(user);

                _logger.LogInformation($"Login successful for username: {user.Name}");

                return new AuthModel
                {
                    Token = token,
                    Role = user.Role.Name,
                    Message = "Login successful",
                };
            }
            else
            {
                throw new Exception("Invalid login attempt. Password is incorrect.");
            }     
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim(ClaimTypes.Role, user.Role.Name)
            };
              
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

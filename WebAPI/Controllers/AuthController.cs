using BusinessLayer.Services.AuthService;
using DomainLayer.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthModelInputDto inputDto)
        {
            try
            {
                var result = await _authService.LoginAsync(inputDto.Email, inputDto.Password);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

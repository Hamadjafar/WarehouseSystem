using BusinessLayer.Services.LogService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : Controller
    {
        private readonly LogsService _logService;

        public LogsController(LogsService logService)
        {
            _logService = logService;
        }

        [HttpGet("GetLogs")]
        public async Task<IActionResult> GetLogs(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _logService.GetAllLogsAsync(pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}

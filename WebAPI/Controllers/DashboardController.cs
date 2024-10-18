using BusinessLayer.Services.DashboardService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("GetWareHouseStatus")]
        public async Task<IActionResult> GetWareHouseStatus()
        {
            try
            {
                var result = await _dashboardService.GetWareHouseStatusAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetTopHighAndLowItemsByQuantity")]
        public async Task<IActionResult> GetTopHighAndLowItemsByQuantity()
        {
            try
            {
                var result = await _dashboardService.GetTopHighAndLowItemsByQuantityAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

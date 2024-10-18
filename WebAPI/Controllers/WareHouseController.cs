using BusinessLayer.Services.WareHouseService;
using DomainLayer.Dtos.wareHousDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WareHouseController : Controller
    {
        private readonly WareHouseService _warehouseService;
        private readonly WareHouseItemsService _wareHouseItemsService;

        public WareHouseController(WareHouseService warehouseService, WareHouseItemsService wareHouseItemsService)
        {
            _warehouseService = warehouseService;
            _wareHouseItemsService = wareHouseItemsService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateWarehouses([FromBody] WareHouseInputDto wareHouseInput)
        {
            try
            {
                await _warehouseService.CreateWareHouseAsync(wareHouseInput);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetAllWarehouses")]
        public async Task<IActionResult> GetAllWarehouses(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _warehouseService.GetAllWareHousesAsync(pageNumber, pageSize);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetItemsById")]
        public async Task<IActionResult> GetItemsById(int warehouseId, int pageNumber, int pageSize)
        {
            try
            {
                var result = await _wareHouseItemsService.GetItemsByIdAsync(warehouseId, pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }
    }
}

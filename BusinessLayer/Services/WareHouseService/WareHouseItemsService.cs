using DataAccessLayer.IRepositories;
using DomainLayer.Dtos.wareHousDto;
using Microsoft.Extensions.Logging;


namespace BusinessLayer.Services.WareHouseService
{
    public class WareHouseItemsService
    {
        private readonly IWareHouseItemsRepository _wareHouseItemsRepository;
        private readonly ILogger<WareHouseItemsService> _logger;
        public WareHouseItemsService(IWareHouseItemsRepository wareHouseItemsRepository, ILogger<WareHouseItemsService> logger)
        {
            _wareHouseItemsRepository = wareHouseItemsRepository;
            _logger = logger;
        }
        public async Task<WareHouseItemsOutputDto> GetItemsByIdAsync(int warehouseId, int pageNumber, int pageSize)
        {
            _logger.LogInformation("Fetching all warehouses items.");
            var items = await _wareHouseItemsRepository.GetItemsById(warehouseId, pageNumber, pageSize);
            _logger.LogInformation($"Successfully retrieved {items.ItemsCount} warehouses items.");
            return items;
        }
    }
}

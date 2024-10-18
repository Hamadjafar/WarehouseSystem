using DataAccessLayer.IRepositories;
using DomainLayer.Dtos;

namespace BusinessLayer.Services.DashboardService
{
    public class DashboardService
    {
        private readonly IWareHouseRepository _warehouseRepository;
        private readonly IWareHouseItemsRepository _wareHouseItemsRepository;
        public DashboardService(IWareHouseRepository warehouseRepository, IWareHouseItemsRepository wareHouseItemsRepository)
        {
            _warehouseRepository = warehouseRepository;
            _wareHouseItemsRepository = wareHouseItemsRepository;
        }

        public async Task<List<WareHouseStatusDto>> GetWareHouseStatusAsync()
        {
            return await _warehouseRepository.GetWarehouseItemsStatus();
        }
        public async Task<TopHighAndLowItemsDto> GetTopHighAndLowItemsByQuantityAsync()
        {
            return await _wareHouseItemsRepository.GetTopHighAndLowItemsByQuantity();
        }
    }
}

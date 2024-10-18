using DomainLayer.Dtos;
using DomainLayer.Entities;


namespace DataAccessLayer.IRepositories
{
    public interface IWareHouseRepository
    {
        Task CreateWarehouse(Warehouse warehouseDto);
        Task<WarehouseOutputDto> GetAllWareHouses(int pageNumber, int pageSize);
        Task Save();
        Task<bool> IsWarehouseNameExists(string name);

        Task<List<WareHouseStatusDto>> GetWarehouseItemsStatus();
        Task Update(Warehouse warehouse);
    }
}

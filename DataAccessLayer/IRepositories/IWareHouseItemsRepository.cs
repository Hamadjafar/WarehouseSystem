using DomainLayer.Dtos;
using DomainLayer.Dtos.wareHousDto;

namespace DataAccessLayer.IRepositories
{
    public interface IWareHouseItemsRepository
    {
        Task<WareHouseItemsOutputDto> GetItemsById(int id, int pageNumber, int pageSize);
        Task<TopHighAndLowItemsDto> GetTopHighAndLowItemsByQuantity();
    }
}

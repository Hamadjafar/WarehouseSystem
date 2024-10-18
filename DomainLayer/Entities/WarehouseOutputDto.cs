using DomainLayer.Dtos.wareHousDto;

namespace DomainLayer.Entities
{
    public class WarehouseOutputDto
    {
        public List<WareHouseOutputDto> WareHousesDto {  get; set; }
        public int TotalItems { get; set; }
    }
}

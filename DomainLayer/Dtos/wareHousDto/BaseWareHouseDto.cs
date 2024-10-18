namespace DomainLayer.Dtos.wareHousDto
{
    public class BaseWareHouseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        public List<WareHouseItemsDto> WareHouseItemsDto { get; set; }
    }
}

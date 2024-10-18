namespace DomainLayer.Dtos
{
    public class TopHighAndLowItemsDto
    {
        public List<ItemsQuantityDto> TopHighItems { get; set; }
        public List<ItemsQuantityDto> TopLowItems { get; set; }

    }
}

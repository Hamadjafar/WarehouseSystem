namespace DomainLayer.Dtos.wareHousDto
{
    public class WareHouseItemsDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string SkuCode { get; set; }
        public int Qty { get; set; }
        public decimal CostPrice { get; set; }
        public decimal? MsrpPrice { get; set; }
        public int WareHouseId { get; set; }
    }
}

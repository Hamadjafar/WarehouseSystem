using System.ComponentModel.DataAnnotations;
namespace DomainLayer.Entities
{
    public class WarehouseItems
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string SkuCode { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Qty { get; set; }
        public decimal CostPrice { get; set; }
        public decimal? MsrpPrice { get; set; }
        public int WareHouseId { get; set; }
        public Warehouse WareHouse { get; set; }
    }
}

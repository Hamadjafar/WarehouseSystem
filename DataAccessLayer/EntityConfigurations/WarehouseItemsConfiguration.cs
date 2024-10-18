using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityConfigurations
{
    public class WarehouseItemsConfiguration : IEntityTypeConfiguration<WarehouseItems>
    {
        public void Configure(EntityTypeBuilder<WarehouseItems> builder)
        {
            builder.ToTable("WarehouseItems");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.ItemName).IsRequired();
            builder.HasIndex(w => w.ItemName).IsUnique();

            builder.Property(i => i.SkuCode).IsRequired(false); ;

            builder.Property(i => i.Qty).IsRequired();

            builder.Property(i => i.CostPrice).IsRequired();

            builder.Property(i => i.MsrpPrice).IsRequired(false);

            builder.HasOne(i => i.WareHouse)
                .WithMany(w => w.Items)
                .HasForeignKey(i => i.WareHouseId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}

using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataAccessLayer.EntityConfigurations
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouse");
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Name).IsRequired();
            builder.HasIndex(w => w.Name).IsUnique();
            builder.Property(w => w.Address).IsRequired();
            builder.Property(w => w.City).IsRequired();
            builder.Property(w => w.CountryId).IsRequired();

            builder.HasMany(w => w.Items)
                .WithOne(i => i.WareHouse)
                .HasForeignKey(i => i.WareHouseId)
                .OnDelete(DeleteBehavior.Cascade);  
        }
    }
}

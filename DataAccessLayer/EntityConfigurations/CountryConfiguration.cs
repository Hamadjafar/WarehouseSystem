using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataAccessLayer.EntityConfigurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasData(
                new Country { Id = 1, Name = "Jordan" },
                new Country { Id = 2, Name = "Egypt" },
                new Country { Id = 3, Name = "Syria" },
                new Country { Id = 4, Name = "Palestine" },
                new Country { Id = 5, Name = "Saudi arabia" },
                new Country { Id = 6, Name = "United Arab Emirates" },
                new Country { Id = 7, Name = "kuwait" },
                new Country { Id = 8, Name = "Oman" },
                new Country { Id = 9, Name = "Bahrain" },
                new Country { Id = 10, Name = "Lebanon" },
                new Country { Id = 11, Name = "Yemen" },
                new Country { Id = 12, Name = "Iraq" },
                new Country { Id = 13, Name = "Qatar" }
            );
        }
    }
}

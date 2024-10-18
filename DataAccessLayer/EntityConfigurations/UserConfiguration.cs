using DomainLayer.Entities;
using DomainLayer.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Name).IsRequired();
            builder.HasOne(u => u.Role)
                  .WithMany()
                  .HasForeignKey(c => c.RoleId)
                  .IsRequired();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.IsActive).IsRequired();

            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher();

            builder.HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@happywarehouse.com",
                    Name = "Hamad Jafar",
                    RoleId = 1, 
                    PasswordHash = hasher.HashPassword("P@ssw0rd"), 
                    IsActive = true
                });
        }
    }
}

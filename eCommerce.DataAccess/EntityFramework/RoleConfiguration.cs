using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.DataAccess.EntityFramework
{
    class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> modelBuilder)
        {
            modelBuilder.ToTable("Role");

            modelBuilder.HasIndex(e => e.RoleName)
                    .HasName("UQ__Role__8A2B6160D5EB574B")
                    .IsUnique();

            modelBuilder.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(6);
        }
    }
}

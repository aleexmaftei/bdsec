using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.DataAccess.EntityFramework
{
    class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> modelBuilder)
        {
            modelBuilder.ToTable("UserRole");

            modelBuilder.HasKey(e => new { e.UserId, e.RoleId })
                   .HasName("PK__UserRole__AF2760ADC859CF22");

            modelBuilder.HasOne(d => d.Role)
                .WithMany(p => p.UserRole)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserRole__RoleId__71BCD978");

            modelBuilder.HasOne(d => d.User)
                .WithMany(p => p.UserRole)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRole__UserId__70C8B53F");
        }
    }
}

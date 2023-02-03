using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.DataAccess.EntityFramework
{
    public class SaltConfiguration : IEntityTypeConfiguration<Salt>
    {
        public void Configure(EntityTypeBuilder<Salt> modelBuilder)
        {
            modelBuilder.ToTable("Salt");

            modelBuilder.HasIndex(e => e.UserId)
                    .HasName("UQ__Salt__1788CC4DF28DFD63")
                    .IsUnique();

            modelBuilder.Property(e => e.SaltPass)
                .IsRequired();

        }
    }
}

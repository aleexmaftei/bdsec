using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.DataAccess.EntityFramework
{
    public class DeliveryLocationConfiguration : IEntityTypeConfiguration<DeliveryLocation>
    {
        public void Configure(EntityTypeBuilder<DeliveryLocation> modelBuilder)
        {
            modelBuilder.ToTable("DeliveryLocation");

            modelBuilder.Property(e => e.AddressDetail).IsRequired();

            modelBuilder.Property(e => e.CityName)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Property(e => e.CountryName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Property(e => e.RegionName)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.HasOne(d => d.User)
                .WithMany(p => p.DeliveryLocation)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__DeliveryL__UserI__6B0FDBE9");

            modelBuilder.Property(e => e.PostalCode)
                .IsRequired();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.DataAccess.EntityFramework
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> modelBuilder)
        {
            modelBuilder.ToTable("Product");

            modelBuilder.HasIndex(e => e.ProductName)
                    .HasName("UQ__Product__DD5A978AA86160BB")
                    .IsUnique();

            modelBuilder.Property(e => e.ProductImage).IsRequired();

            modelBuilder.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Property(e => e.Quantity).HasDefaultValueSql("((1))");

            modelBuilder.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ManufacturerId)
                    .HasConstraintName("FK__Product__Manufac__44952D46");
        }
    }
}

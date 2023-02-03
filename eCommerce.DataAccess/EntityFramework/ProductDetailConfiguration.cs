using eCommerce.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Data.EntityFramework
{
    class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetail>
    {
        public void Configure(EntityTypeBuilder<ProductDetail> modelBuilder)
        {
            modelBuilder.ToTable("ProductDetail");

            modelBuilder.HasKey(e => new { e.ProductId, e.ProductCategoryId, e.PropertyId })
                    .HasName("PK__ProductD__735E41A6D327741A");

            modelBuilder.Property(e => e.DetailValue).IsRequired();

            modelBuilder.HasOne(d => d.ProductCategory)
                .WithMany(p => p.ProductDetail)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK__ProductDe__Produ__664B26CC");

            modelBuilder.HasOne(d => d.Product)
                .WithMany(p => p.ProductDetail)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductDe__Produ__65570293");

            modelBuilder.HasOne(d => d.Property)
                .WithMany(p => p.ProductDetail)
                .HasForeignKey(d => d.PropertyId)
                .HasConstraintName("FK__ProductDe__Prope__673F4B05");
        }
    }
}

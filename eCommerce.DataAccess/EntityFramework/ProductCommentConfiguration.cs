using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.DataAccess.EntityFramework
{
    public class ProductCommentConfiguration : IEntityTypeConfiguration<ProductComment>
    {
        public void Configure(EntityTypeBuilder<ProductComment> modelBuilder)
        {
            modelBuilder.ToTable("ProductComment");

            modelBuilder.Property(e => e.CommentDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

            modelBuilder.Property(e => e.CommentTitle)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Property(e => e.ProductRating).HasDefaultValueSql("((5))");

            modelBuilder.Property(e => e.UserNameComment)
                .IsRequired()
                .HasMaxLength(35);

            modelBuilder.HasOne(d => d.Product)
                .WithMany(p => p.ProductComment)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductCo__Produ__4959E263");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.DataAccess.EntityFramework
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> modelBuilder)
        {
            modelBuilder.ToTable("Cart");

            modelBuilder.HasKey(e => new { e.UserId, e.ProductId })
                    .HasName("PK__Cart__DCC800202AC818CE");

            modelBuilder.Property(e => e.QuantityBuy).HasDefaultValueSql("((1))");

            modelBuilder.HasOne(d => d.Product)
                .WithMany(p => p.Cart)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Cart__ProductId__758D6A5C");

            modelBuilder.HasOne(d => d.User)
                .WithMany(p => p.Cart)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Cart__UserId__74994623");
        }
    }
}

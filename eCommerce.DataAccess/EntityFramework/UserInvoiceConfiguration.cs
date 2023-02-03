using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.DataAccess.EntityFramework
{
    public class UserInvoiceConfiguration : IEntityTypeConfiguration<UserInvoice>
    {
        public void Configure(EntityTypeBuilder<UserInvoice> modelBuilder)
        {
            modelBuilder.ToTable("UserInvoice");

            modelBuilder.HasKey(e => new { e.UserInvoiceId, e.ProductId, e.DeliveryLocationId })
                    .HasName("PK__UserInvo__5151C2FAB536B29D");

            modelBuilder.Property(e => e.DateBuy)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            modelBuilder.Property(e => e.QuantityBuy).HasDefaultValueSql("((1))");
        }
    }
}

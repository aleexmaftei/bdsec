using eCommerce.Data.EntityFramework;
using eCommerce.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;

namespace eCommerce.DataAccess
{
    public partial class EcommerceContext : DbContext
    {
        public EcommerceContext()
        {
        }

        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<DeliveryLocation> DeliveryLocation { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductComment> ProductComment { get; set; }
        public virtual DbSet<ProductDetail> ProductDetail { get; set; }
        public virtual DbSet<Properties> Properties { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserInvoice> UserInvoice { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserT> UserT { get; set; }
        public virtual DbSet<Salt> Salt { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local); Initial Catalog=eCommerce; Integrated Security=SSPI; Application Name=eCommerce; MultipleActiveResultSets=true; Pooling=true; Max Pool Size=100;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
            modelBuilder.ApplyConfiguration(new PropertiesConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCommentConfiguration());

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SaltConfiguration());
            modelBuilder.ApplyConfiguration(new UserInvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveryLocationConfiguration());

            modelBuilder.ApplyConfiguration(new CartConfiguration());
            
            
        }

    }
}
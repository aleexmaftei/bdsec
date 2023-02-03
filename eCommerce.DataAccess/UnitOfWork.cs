using eCommerce.DataAccess;

namespace eCommerce.Data
{
    public class UnitOfWork
    {
        private readonly EcommerceContext Context;
        public UnitOfWork(EcommerceContext context)
        {
            this.Context = context;
        }

        private BaseRepository<Product> products;
        private BaseRepository<ProductDetail> productDetails;
        private BaseRepository<Properties> properties;
        private BaseRepository<ProductCategory> productCategories;
        private BaseRepository<ProductComment> productComments;

        public BaseRepository<Product> Products => products ??= new BaseRepository<Product>(Context);
        public BaseRepository<ProductDetail> ProductDetails => productDetails ??= new BaseRepository<ProductDetail>(Context);
        public BaseRepository<Properties> Properties => properties ??= new BaseRepository<Properties>(Context);
        public BaseRepository<ProductCategory> ProductCategories => productCategories ??= new BaseRepository<ProductCategory>(Context);
        public BaseRepository<ProductComment> ProductComments => productComments ??= new BaseRepository<ProductComment>(Context);

        
        private BaseRepository<UserT> users;
        private BaseRepository<Salt> salts;
        private BaseRepository<DeliveryLocation> deliveryLocations;
        private BaseRepository<Role> roles;
        private BaseRepository<UserRole> userRoles;
        
        public BaseRepository<UserT> Users => users ??= new BaseRepository<UserT>(Context);
        public BaseRepository<Salt> Salts => salts ??= new BaseRepository<Salt>(Context);
        public BaseRepository<DeliveryLocation> DeliveryLocations => deliveryLocations ??= new BaseRepository<DeliveryLocation>(Context);
        public BaseRepository<Role> Roles => roles ??= new BaseRepository<Role>(Context);
        public BaseRepository<UserRole> UserRoles => userRoles ??= new BaseRepository<UserRole>(Context);


        private BaseRepository<Cart> carts;
        private BaseRepository<UserInvoice> userInvoices;

        public BaseRepository<Cart> Carts => carts ??= new BaseRepository<Cart>(Context);
        public BaseRepository<UserInvoice> UserInvoices => userInvoices ??= new BaseRepository<UserInvoice>(Context);

        private BaseRepository<Manufacturer> manufacturers;
        public BaseRepository<Manufacturer> Manufacturers => manufacturers ??= new BaseRepository<Manufacturer>(Context);

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}

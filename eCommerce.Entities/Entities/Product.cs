using eCommerce.Common;
using System.Collections.Generic;

namespace eCommerce.DataAccess
{
    public partial class Product : IEntity
    {
        public Product()
        {
            Cart = new HashSet<Cart>();
            ProductComment = new HashSet<ProductComment>();
            ProductDetail = new HashSet<ProductDetail>();
            UserInvoice = new HashSet<UserInvoice>();

        }

        public int ProductId { get; set; }
        public int ManufacturerId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public double ProductPrice { get; set; }
        public bool IsDeleted { get; set; }
        public int Quantity { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<ProductComment> ProductComment { get; set; }
        public virtual ICollection<ProductDetail> ProductDetail { get; set; }
        public virtual ICollection<UserInvoice> UserInvoice { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
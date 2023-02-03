using eCommerce.Common;

namespace eCommerce.DataAccess
{
    public partial class ProductDetail : IEntity
    {
        public int ProductId { get; set; }
        public int ProductCategoryId { get; set; }
        public int PropertyId { get; set; }
        public string DetailValue { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual Properties Property { get; set; }
    }
}
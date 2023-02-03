using eCommerce.Common;
using System.Collections.Generic;

namespace eCommerce.DataAccess
{
    public partial class ProductCategory : IEntity
    {
        public ProductCategory()
        {
            InverseParentProductCategory = new HashSet<ProductCategory>();
            ProductDetail = new HashSet<ProductDetail>();
        }

        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductCategoryImage { get; set; }
        public int? ParentProductCategoryId { get; set; }

        public virtual ProductCategory ParentProductCategory { get; set; }
        public virtual ICollection<ProductCategory> InverseParentProductCategory { get; set; }
        public virtual ICollection<ProductDetail> ProductDetail { get; set; }
    }
}
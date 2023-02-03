using eCommerce.DataAccess;
using System.Collections.Generic;

namespace eCommerce.Models.ProductVM
{
    public class ProductCategoryVM
    {
        public string TitleCategory { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Manufacturer> Manufacturers { get; set; }
    }
}

using System.Collections.Generic;

namespace eCommerce.Entities.Entities.ProductAdmin
{
    public class AdminAdd
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int ProductCategoryId { get; set; }
        public double ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int ManufacturerId { get; set; }

        public List<ProductPropertiesWithValues> ProductPropertiesWithValues { get; set; }

        public AdminAdd()
        {
            ProductPropertiesWithValues = new List<ProductPropertiesWithValues>();
        }
    }
}

using System.Collections.Generic;

namespace eCommerce.Entities.Entities.ProductAdmin
{
    public class AdminUpdate
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductId { get; set; }
        public bool IsDeleted { get; set; }
        public List<ProductPropertiesWithValues> ProductPropertiesWithValues { get; set; }

        public AdminUpdate()
        {
            ProductPropertiesWithValues = new List<ProductPropertiesWithValues>();
        }
    }
}

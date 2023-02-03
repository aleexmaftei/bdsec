using System.Collections.Generic;

namespace eCommerce.Entities.DTOs.AdminDtos
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int ProductCategoryId { get; set; }
        public double ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int ManufacturerId { get; set; }
        public List<ProductPropertiesWithValuesDto> ProductPropertiesWithValues { get; set; }

        public ProductDto()
        {
            ProductPropertiesWithValues = new List<ProductPropertiesWithValuesDto>();
        }
    }
}

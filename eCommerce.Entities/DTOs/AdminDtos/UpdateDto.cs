using System.Collections.Generic;

namespace eCommerce.Entities.DTOs.AdminDtos
{
    public class UpdateDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int ProductCategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public int ProductId { get; set; }
        public List<ProductPropertiesWithValuesDto> ProductPropertiesWithValues { get; set; }

        public UpdateDto()
        {
            ProductPropertiesWithValues = new List<ProductPropertiesWithValuesDto>();
        }
    }
}

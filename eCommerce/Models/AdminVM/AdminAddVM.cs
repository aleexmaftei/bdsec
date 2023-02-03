using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models.AdminVM
{
    public class AdminAddVM
    {
        [Required(ErrorMessage = "Mandatory!")]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        
        [RegularExpression("([1-9][0-9]*)")]
        public int Quantity { get; set; }
        
        public int ProductCategoryId { get; set; }
        
        [Required(ErrorMessage = "Mandatory!")]
        public double ProductPrice { get; set; }

        [DisplayName("Insert product image")]
        [Required(ErrorMessage = "Mandatory!")]
        public string ProductImage { get; set; }
        
        public int ManufacturerId { get; set; }

        public List<ProductPropertiesWithValuesVM> ProductPropertiesWithValues { get; set; }

        public AdminAddVM()
        {
            ProductPropertiesWithValues = new List<ProductPropertiesWithValuesVM>();
        }
    }
}

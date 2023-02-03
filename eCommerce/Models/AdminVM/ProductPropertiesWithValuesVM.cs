using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models.AdminVM
{
    public class ProductPropertiesWithValuesVM
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        [Required(ErrorMessage = "Mandatory!")]
        public string DetailValue { get; set; }
    }
}

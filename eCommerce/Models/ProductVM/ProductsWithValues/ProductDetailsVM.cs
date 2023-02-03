using System.Collections.Generic;

namespace eCommerce.Models.ProductVM.ProductsWithValues
{
    public class ProductDetailsVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int QuantityBuy { get; set; }
        public int Quantity { get; set; }
        public string ManufacturerLogo { get; set; }
        public string ManufacturerName { get; set; }

        public string CategoryId { get; set; }

        public List<PropertyWithValueVM> AllPropertiesWithValues;
        public ProductDetailsVM()
        {
            AllPropertiesWithValues = new List<PropertyWithValueVM>();
        }
    }
}

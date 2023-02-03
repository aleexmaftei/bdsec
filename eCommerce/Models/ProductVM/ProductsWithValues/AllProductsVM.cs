using eCommerce.Models.AdminVM;
using System.Collections.Generic;

namespace eCommerce.Models.ProductVM.ProductsWithValues
{
    public class AllProductsVM
    {
        public int CategoryId { get; set; }

        public AdminUpdateVM AdminUpdateDetails { get; set; }
        public int ProductsCount { get; set; }
        public List<ProductDetailsVM> Allproducts { get; set; }
        public List<ManufacturerVM> Manufacturers { get; set; }

        public AllProductsVM()
        {
            Allproducts = new List<ProductDetailsVM>();
            Manufacturers = new List<ManufacturerVM>();
        }
    }
}

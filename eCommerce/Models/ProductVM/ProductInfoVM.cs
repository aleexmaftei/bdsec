using eCommerce.Models.ProductVM.ProductsWithValues;
using System.Collections.Generic;

namespace eCommerce.Models.ProductVM
{
    public class ProductInfoVM
    {
        public int NumberOfEvaluations { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public int Quantity { get; set; }

        public double AvgStars { get; set; }

        public ProductDetailsVM ProductDetails { get; set; }

        public List<ProductCommentVM> ProductComments { get; set; }

        public ProductInfoVM()
        {
            ProductComments = new List<ProductCommentVM>();
        }
    }
}

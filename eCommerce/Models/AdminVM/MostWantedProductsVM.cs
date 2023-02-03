using eCommerce.Entities.Entities.ProductAdmin;
using System.Collections.Generic;

namespace eCommerce.Models.AdminVM
{
    public class MostWantedProductsVM
    {
        public List<MostWantedProducts> MostWanted { get; set; }
        
        public MostWantedProductsVM()
        {
            MostWanted = new List<MostWantedProducts>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models.AdminVM
{
    public class AdminUpdateVM
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductId { get; set; }
        public bool IsDeleted { get; set; }
        public List<ProductPropertiesWithValuesVM> ProductPropertiesWithValues { get; set; }

        public AdminUpdateVM()
        {
            ProductPropertiesWithValues = new List<ProductPropertiesWithValuesVM>();
        }
    }
}

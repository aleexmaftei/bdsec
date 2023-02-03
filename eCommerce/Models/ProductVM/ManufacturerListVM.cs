using System.Collections.Generic;

namespace eCommerce.Models.ProductVM
{
    public class ManufacturerListVM
    {
        public int ParentCategoryId { get; set; }
        public int CategoryId { get; set; }

        public List<ManufacturerVM> ManufacturerList { get; set; }

        public ManufacturerListVM()
        {
            ManufacturerList = new List<ManufacturerVM>();
        }
    }
}

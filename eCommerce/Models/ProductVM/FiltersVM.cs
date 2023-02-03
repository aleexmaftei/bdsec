using System.Collections.Generic;

namespace eCommerce.Models.ProductVM
{
    public class FiltersVM
    {
        public List<string> ManufacturersFilter { get; set; }
        public double MinPriceFilter { get; set; }
        public double MaxPriceFilter { get; set; }

        public FiltersVM()
        {
            ManufacturersFilter = new List<string>();
        }
    }
}

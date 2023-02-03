using System.Collections.Generic;

namespace eCommerce.Entities.Entities
{
    public class Filters
    {
        public List<string> ManufacturersFilter { get; set; }
        public double MinPriceFilter { get; set; }
        public double MaxPriceFilter { get; set; }

        public Filters()
        {
            ManufacturersFilter = new List<string>();
        }
    }
}

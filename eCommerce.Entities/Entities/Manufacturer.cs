using eCommerce.Common;
using System.Collections.Generic;

namespace eCommerce.DataAccess
{
    public partial class Manufacturer : IEntity
    {
        public Manufacturer()
        {
            Product = new HashSet<Product>();
        }

        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerLogo { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
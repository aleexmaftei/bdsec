using eCommerce.Common;
using System.Collections.Generic;

namespace eCommerce.DataAccess
{
    public partial class DeliveryLocation : IEntity
    {
        public DeliveryLocation()
        {
            UserInvoice = new HashSet<UserInvoice>();
        }

        public int DeliveryLocationId { get; set; }
        public int UserId { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string CityName { get; set; }
        public string AddressDetail { get; set; }
        public int PostalCode { get; set; }

        public virtual UserT User { get; set; }
        public virtual ICollection<UserInvoice> UserInvoice { get; set; }
    }
}
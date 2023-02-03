using System.Collections.Generic;

namespace eCommerce.Models.MyProfileVM.DeliveryLocation
{
    public class ListDeliveryLocationVm
    {
        public List<DeliveryLocationVm> DeliveryLocations { get; set; }

        public ListDeliveryLocationVm()
        {
            DeliveryLocations = new List<DeliveryLocationVm>();
        }
    }
}

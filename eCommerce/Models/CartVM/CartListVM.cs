using eCommerce.Models.MyProfileVM.DeliveryLocation;
using System.Collections.Generic;

namespace eCommerce.Models.CartVM
{
    public class CartListVM
    {
        public double TotalSum { get; set; }
        public List<CartVM> CartList { get; set; }

        public List<DeliveryLocationVm> DeliveryLocations { get; set; }

        public CartListVM()
        {
            CartList = new List<CartVM>();
            DeliveryLocations = new List<DeliveryLocationVm>();
        }
    }
}

using eCommerce.DataAccess;
using eCommerce.Models.MyProfileVM.DeliveryLocation;
using eCommerce.Models.MyProfileVM.Invoice;
using System.Collections.Generic;

namespace eCommerce.Models.MyProfileVM
{
    public class MyProfileVm
    {
        public UserInvoiceVm LastInvoice { get; set; }

        public List<DeliveryLocationVm> DeliveryLocations { get; set; }

        public MyProfileVm()
        {
            DeliveryLocations = new List<DeliveryLocationVm>();
        }
    }
}

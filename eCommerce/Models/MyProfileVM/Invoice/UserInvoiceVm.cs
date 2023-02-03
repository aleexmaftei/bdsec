using System;

namespace eCommerce.Models.MyProfileVM.Invoice
{
    public class UserInvoiceVm
    {
        public int ProductId { get; set; }

        public DateTime DateBuy { get; set; }

        public int QuantityBuy { get; set; }

        public string AdressDetail { get; set; }

        public string CityName { get; set; }

        public string RegionName { get; set; }
    }
}

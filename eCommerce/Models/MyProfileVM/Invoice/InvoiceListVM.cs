using eCommerce.Models.MyProfileVM.Invoice;
using System;
using System.Collections.Generic;

namespace eCommerce.Models.MyProfileVM
{
    public class InvoiceListVM
    {
        public int UserInvoiceId { get; set; }
        public DateTime DateBuy { get; set; }
        public List<OneInvoicePropertiesVM> InvoiceList { get; set; }

        public InvoiceListVM()
        {
            InvoiceList = new List<OneInvoicePropertiesVM>();
        }
    }
}

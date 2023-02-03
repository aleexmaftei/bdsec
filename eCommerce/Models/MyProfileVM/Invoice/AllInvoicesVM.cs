using System.Collections.Generic;

namespace eCommerce.Models.MyProfileVM
{
    public class AllInvoicesVM
    {
        public List<InvoiceListVM> AllInvoices { get; set; }

        public AllInvoicesVM()
        {
            AllInvoices = new List<InvoiceListVM>();
        }
    }
}

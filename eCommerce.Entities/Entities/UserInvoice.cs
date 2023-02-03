using eCommerce.Common;
using System;

namespace eCommerce.DataAccess
{
    public partial class UserInvoice : IEntity
    {
        public int UserInvoiceId { get; set; }
        public int ProductId { get; set; }
        public int DeliveryLocationId { get; set; }
        public DateTime DateBuy { get; set; }
        public int QuantityBuy { get; set; }

        public virtual DeliveryLocation DeliveryLocation { get; set; }
        public virtual Product Product { get; set; }
    }
}
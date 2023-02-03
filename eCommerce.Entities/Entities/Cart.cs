using eCommerce.Common;

namespace eCommerce.DataAccess
{
    public partial class Cart : IEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int QuantityBuy { get; set; }

        public virtual Product Product { get; set; }
        public virtual UserT User { get; set; }
    }
}
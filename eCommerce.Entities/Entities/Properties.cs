using eCommerce.Common;
using System.Collections.Generic;

namespace eCommerce.DataAccess
{
    public partial class Properties : IEntity
    {
        public Properties()
        {
            ProductDetail = new HashSet<ProductDetail>();
        }

        public int PropertyId { get; set; }
        public int ProductBaseCategoryId { get; set; }
        public string PropertyName { get; set; }

        public virtual ICollection<ProductDetail> ProductDetail { get; set; }
    }
}
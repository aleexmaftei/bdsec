using eCommerce.Common;

namespace eCommerce.DataAccess
{
    public partial class Salt : IEntity
    {
        public int SaltId { get; set; }
        public int UserId { get; set; }
        public string SaltPass { get; set; }

        public virtual UserT User { get; set; }
    }
}
using eCommerce.Common;

namespace eCommerce.DataAccess
{
    public partial class UserRole : IEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual UserT User { get; set; }
    }
}
using eCommerce.Common;
using System.Collections.Generic;

namespace eCommerce.DataAccess
{
    public partial class UserT : IEntity
    {
        public UserT()
        {
            Cart = new HashSet<Cart>();
            DeliveryLocation = new HashSet<DeliveryLocation>();
            UserRole = new HashSet<UserRole>();
            Salt = new HashSet<Salt>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<DeliveryLocation> DeliveryLocation { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual ICollection<Salt> Salt { get; set; }
    }
}
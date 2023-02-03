using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models.AdminVM
{
    public class AdminSoftDeleteVM
    {
        [Required]
        public int ProductId { get; set; }
    }
}

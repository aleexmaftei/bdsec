using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models.UserAccountVM
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool AreCredentialsInvalid { get; set; }
    }
}

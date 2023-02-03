using eCommerce.Code.CustomAttributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models.UserAccountVM
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Email obligatoriu")]
        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola obligatoriu")]
        [PasswordPropertyText]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Prenume obligatoriu")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nume obligatoriu")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Nume obligatoriu")]
        [DisplayName("Country")]
        public string CountryName { get; set; }

        [Required(ErrorMessage = "Regiune obligatoriu")]
        [DisplayName("Region")]
        public string RegionName { get; set; }

        [Required(ErrorMessage = "Oras obligatoriu")]
        [DisplayName("City")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Adresa obligatoriu")]
        [DisplayName("Str. Nr. Bl.")]
        public string AddressDetail { get; set; }

        [Required(ErrorMessage = "Cod Postal obligatoriu")]
        //[RegularExpression("([0-9]*)")]
        [PositiveNumber(ErrorMessage = "Only digits!")]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

    }
}

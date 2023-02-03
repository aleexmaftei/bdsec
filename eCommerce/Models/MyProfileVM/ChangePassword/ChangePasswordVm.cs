using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models.MyProfileVM.ChangePassword
{
    public class ChangePasswordVm
    {
        [DisplayName("New Password")]
        [Required(ErrorMessage = "Insert the new password!")]
        public string NewPassword { get; set; }
        
        [DisplayName("Old Password")]
        [Required(ErrorMessage = "Insert the old password!")]
        public string OldPassword { get; set; }
        
        [DisplayName("Confirm new Password")]
        [Required(ErrorMessage = "Insert the password confirmation!")]
        public string ConfirmNewPassword { get; set; }
    }
}

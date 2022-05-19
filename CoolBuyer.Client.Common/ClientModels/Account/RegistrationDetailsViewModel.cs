using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels.Account
{
    public class RegistrationDetailsViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(maximumLength: 20, ErrorMessage = "Invalid username length.", MinimumLength = 6)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(maximumLength: 30, ErrorMessage = "Invalid email length.", MinimumLength = 5)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(maximumLength: 20, ErrorMessage = "Invalid password length.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}

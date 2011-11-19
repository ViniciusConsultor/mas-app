using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shipping.Mvc.Models.OnBoarding
{
    public class IndexModel
    {
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Middle Initial")]
        public string MiddleInitial { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [DisplayName("Change Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [DisplayName("Email Address")]
        public string Email { get; set; }
        
        [DisplayName("Mobile Phone")]
        public string MobilePhoneNumber { get; set; }

        [DisplayName("Business Phone")]
        public string BusinessPhoneNumber { get; set; }
    }
}
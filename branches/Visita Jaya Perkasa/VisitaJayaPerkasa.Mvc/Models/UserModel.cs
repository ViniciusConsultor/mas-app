using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VisitaJayaPerkasa.Mvc.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [DisplayName("Role")]
        public string SelectedRoleName { get; set; }
        public List<SelectListItem> Roles { get; set; }

        [Required(ErrorMessage = "Username Name must be filled")]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password Name must be filled")]
        [DisplayName("Password")]
        public string Password { get; set; }

        public string Salt { get; set; }

        [Required(ErrorMessage = "Last Name must be filled")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name must be filled")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Email must be filled")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter valid email address")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Mobile Number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Mobile Number")]
        public string MobilePhoneNumber { get; set; }

        [DisplayName("NIK")]
        public string Nik { get; set; }

        [DisplayName("Address")]
        public string address { get; set; }

        [DisplayName("Date of Birth")]
        public string DateOfBirth { get; set; }

        [DisplayName("Marital Status")]
        public string MartialStatus { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }

        [ScaffoldColumn(false)]
        public int deleted { get; set; }
    }
}
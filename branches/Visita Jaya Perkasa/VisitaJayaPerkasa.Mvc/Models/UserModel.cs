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

        [MinLength(1, ErrorMessage = "Role must be selected")]
        [DisplayName("Role")]
        public string SelectedRoleName { get; set; }
        public List<SelectListItem> Roles { get; set; }

        [Required(ErrorMessage = "Username must be filled")]
        [MaxLength(30, ErrorMessage = "Max Length 30")]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password must be filled")]
        [MaxLength(256, ErrorMessage = "Max Length 256")]
        [DisplayName("Password")]
        public string Password { get; set; }

        public string Salt { get; set; }

        [Required(ErrorMessage = "Last Name must be filled")]
        [MaxLength(30, ErrorMessage = "Max Length 30")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name must be filled")]
        [MaxLength(30, ErrorMessage = "Max Length 30")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [MaxLength(1, ErrorMessage = "Max Length 1")]
        [DisplayName("Middle Initial")]
        public string MiddleInitial { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter valid email address")]
        [MaxLength(50, ErrorMessage = "Max Length 50")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Phone Number")]
        [MaxLength(15, ErrorMessage = "Max Length 15")]
        public string PhoneNumber { get; set; }

        [DisplayName("Mobile Number")]
        [MaxLength(15, ErrorMessage = "Max Length 15")]
        public string MobilePhoneNumber { get; set; }

        [MaxLength(30, ErrorMessage = "Max Length 30")]
        [DisplayName("NIK")]
        public string Nik { get; set; }

        [MaxLength(250, ErrorMessage = "Max Length 250")]
        [DisplayName("Address")]
        public string Address { get; set; }

        [MaxLength(50, ErrorMessage = "Max Length 50")]
        [DisplayName("Date of Birth")]
        public string DateOfBirth { get; set; }

        [MaxLength(30, ErrorMessage = "Max Length 30")]
        [DisplayName("Marital Status")]
        public string MaritalStatus { get; set; }
        public List<SelectListItem> MaritalStatuses { get; set; }

        [MaxLength(20, ErrorMessage = "Max Length 20")]
        [DisplayName("Gender")]
        public string Gender { get; set; }
        public List<SelectListItem> Genders { get; set; }

        [ScaffoldColumn(false)]
        public int deleted { get; set; }

        [MaxLength(50, ErrorMessage = "Max Length 50")]
        [DisplayName("Starting Date")]
        public string StartingDate { get; set; }

        [DisplayName("Salary/Month")]
        public double SalaryPerMonth { get; set; }

        [DisplayName("UMK/Day")]
        [DisplayFormat(DataFormatString = "{0:#,#.00}")]
        public double UmkPerDay { get; set; }
    }
}
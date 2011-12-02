using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Shipping.Mvc.Models.UserAdm
{
    public class UserAdmModel
    {
        public virtual Guid Id { get; set; }
        [DisplayName("Username")]
        public string Username { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }
        [DisplayName("Salt")]
        public string Salt { get; set; }
        [DisplayName("Password Hint")]
        public string PasswordHint { get; set; }
        [DisplayName("Deleted")]
        public bool IsDeleted { get; set; }
        [DisplayName("Activation Date")]
        public DateTime? ActivationDate { get; set; }
        [DisplayName("Deactivation Date")]
        public DateTime? DeactivationDate { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Initial")]
        public string MiddleInitial { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter valid email address")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Email Verified")]
        public bool IsEmailVerified { get; set; }
        [DisplayName("Last Password Changed")]
        public DateTime? PasswordLastChanged { get; set; }
        [DisplayName("Need To Change Password")]
        public bool IsPasswordChangeRequired { get; set; }
        [DisplayName("Mobile Number")]
        public string MobilePhoneNumber { get; set; }
        [DisplayName("Bussiness Number")]
        public string BusinessPhoneNumber { get; set; }
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
    }
}
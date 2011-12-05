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
        public Guid SelectedRoleId { get; set; }
        public List<SelectListItem> Roles { get; set; }
        
        [DisplayName("Username")]
        public string Username { get; set; }
        
        [DisplayName("Password")]
        public string Password { get; set; }
        
        [DisplayName("Salt")]
        public string Salt { get; set; }
        
        [DisplayName("Last Name")]
        public string LastName { get; set; }
 
        [DisplayName("First Name")]
        public string FirstName { get; set; }
 
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
    }
}
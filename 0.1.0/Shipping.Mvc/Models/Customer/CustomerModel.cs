using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shipping.Mvc.Models.Customer
{
    public class CustomerModel
    {
        [Required(ErrorMessage="Customer Name must be fill")]
        [MinLength(5,ErrorMessage="Customer Name min 5 character")]
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        public string Office { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }

        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }
    }
}
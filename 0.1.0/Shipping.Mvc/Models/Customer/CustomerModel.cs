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
        [ScaffoldColumn(false)]
        public Guid ID { get; set; }

        [Required(ErrorMessage="Customer Name must be fill")]
        [MinLength(5,ErrorMessage="Customer Name min 5 character")]
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        public string Office { get; set; }
        public string Address { get; set; }

        [MaxLength(13, ErrorMessage = "Max Length 13")]
        [MinLength(6, ErrorMessage = "Min Length 6")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Telp just only number 0-9")]
        public string Phone { get; set; }
        public string Fax { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage="Please enter valid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }
    }
}
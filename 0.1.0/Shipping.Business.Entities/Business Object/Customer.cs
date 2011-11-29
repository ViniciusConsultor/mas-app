using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Shipping.Business.Entities
{
    [Table(Name="Customer")]
    public class Customer
    {
        [Column(Name="customer_id", IsPrimaryKey=true)]
        public Guid ID { get; set; }

        [Column(Name="customer_name")]
        public string CustomerName { get; set; }

        [Column(Name = "office")]
        public string Office { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "phone")]
        public string Phone { get; set; }

        [Column(Name = "fax")]
        public string Fax { get; set; }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "contact_person")]
        public string ContactPerson { get; set; }
    }
}

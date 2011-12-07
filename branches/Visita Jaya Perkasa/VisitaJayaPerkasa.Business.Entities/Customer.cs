using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace VisitaJayaPerkasa.Business.Entities
{
    [TableName("CUSTOMER")]
    [PrimaryKey("customer_id", autoIncrement = false)]
    public class Customer
    {
       [Column("customer_id")]
        public Guid ID { get; set; }

        [Column("customer_name")]
        public string CustomerName { get; set; }

        [Column("office")]
        public string Office { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("fax")]
        public string Fax { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("contact_person")]
        public string ContactPerson { get; set; }

        [Column("deleted")]
        public int Deleted { get; set; }
    }
}

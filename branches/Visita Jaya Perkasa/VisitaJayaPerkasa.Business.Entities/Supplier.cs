using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PetaPoco;

namespace VisitaJayaPerkasa.Business.Entities
{
    [TableName("SUPPLIER")]
    [PrimaryKey("supplier_id", autoIncrement = false)]
    public class Supplier
    {
        [Column("supplier_id")]
        public Guid Id { get; set; }

        [Column("category_code")]
        public string CategoryCode { get; set; }

        [Column("supplier_name")]
        public string SupplierName { get; set; }

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

        public List<SupplierDetail> SupplierDetails { get; set; }
    }
}

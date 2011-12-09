using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace VisitaJayaPerkasa.Business.Entities
{
    [TableName("SUPPLIER_DETAIL")]
    [PrimaryKey("supplier_detail_id", autoIncrement = false)]
    public class SupplierDetail : Supplier
    {
        [Column("supplier_detail_id")]
        public Guid SupplierDetailId { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("mobile_phone")]
        public string MobilePhone { get; set; }

        [Column("deleted")]
        public int Deleted { get; set; }
    }
}

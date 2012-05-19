using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class SupplierDetail : Supplier
    {
        public Guid SupplierDetailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SupplierDetailAddress { get; set; }
        public string SupplierMobileExt { get; set; }
        public string SupplierDetailPhone { get; set; }
        public string SupplierDetailMobilePhone { get; set; }
        public string SupplierDetailEmail { get; set; }
        public string SupplierDetailDivision { get; set; }
        public int SupplierDetailDeleted { get; set; }
    }
}

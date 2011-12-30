using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class PriceList
    {
        public Guid ID { get; set; }
        public DateTime Date { get; set; }
        public Guid SupplierID { get; set; }
        public Guid Destination { get; set; }
        public Guid TypeID { get; set; }
        public Guid ConditionID { get; set; }
        public Guid StuffingID { get; set; }
        public Guid Recipient { get; set; }
        public decimal PriceSupplier { get; set; }
        public decimal PriceCustomer { get; set; }
        public Guid CustomerID { get; set; }

    }
}

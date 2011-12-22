using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class CustomerTransDetail : CustomerTrans
    {
        public Guid ID { get; set; }
        public Guid TypeID { get; set; }
        public Guid PelayaranID { get; set; }
        public Guid Origin { get; set; }
        public Guid Destination { get; set; }
        public decimal Price { get; set; }
        public Guid ConditionID { get; set; }
        public string NoSeal { get; set; }
        public int CustomerTransDetailDeleted { get; set; }
    }
}

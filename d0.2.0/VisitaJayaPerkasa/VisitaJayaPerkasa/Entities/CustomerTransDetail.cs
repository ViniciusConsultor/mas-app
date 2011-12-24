using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class CustomerTransDetail : CustomerTrans
    {
        public Guid CustomerDetailTransID { get; set; }
        public Guid TypeID { get; set; }
        public Guid PelayaranID { get; set; }
        public Guid Origin { get; set; }
        public Guid Destination { get; set; }
        public decimal Price { get; set; }
        public Guid ConditionID { get; set; }
        public string NoSeal { get; set; }
        public int CustomerTransDetailDeleted { get; set; }

        //model field for view
        public string TypeName { get; set; }
        public string PelayaranName { get; set; }
        public string OriginName { get; set; }
        public string DestinationName { get; set; }
        public string ConditionName { get; set; }
    }
}

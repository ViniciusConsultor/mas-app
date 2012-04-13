using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class CustomerTransDetailSimplified : CustomerTrans
    {
        public Guid CustomerDetailTransID { get; set; }
        //model field for view
        public string TypeName { get; set; }
        public string VesselName { get; set; }
        public string OriginName { get; set; }
        public string DestinationName { get; set; }
        public string ConditionName { get; set; }
        public string WarehouseName { get; set; }
        public string RecipientName { get; set; }
        public string StringRep
        {
            get { return TypeName + ";" + OriginName + ">" + DestinationName + ";" + RecipientName; }
        }
    }
}

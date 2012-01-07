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
        public Guid PelayaranDetailID { get; set; }
        public Guid Origin { get; set; }
        public Guid Destination { get; set; }
        public Guid ConditionID { get; set; }
        public string NoSeal { get; set; }
        public string Voyage { get; set; }
        public string TruckNo { get; set; }
        public DateTime StuffingDate { get; set; }
        public string StuffingPlace { get; set; }
        public DateTime ETD { get; set; }
        public DateTime TD { get; set; }
        public DateTime ETA { get; set; }
        public DateTime TA { get; set; }
        public DateTime Unloading { get; set; }
        public int CustomerTransDetailDeleted { get; set; }

        //model field for view
        public string TypeName { get; set; }
        public string VesselName { get; set; }
        public string OriginName { get; set; }
        public string DestinationName { get; set; }
        public string ConditionName { get; set; }
    }
}

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
        public decimal Price { get; set; }
        public string Voyage { get; set; }
        public string TruckNo { get; set; }
        public DateTime StuffingDate { get; set; }
        public Guid StuffingPlace { get; set; }
        public DateTime ETD { get; set; }
        public DateTime TD { get; set; }
        public DateTime ETA { get; set; }
        public DateTime TA { get; set; }
        public DateTime Unloading { get; set; }
        public Guid RecipientID { get; set; }
        public string JenisBarang { get; set; }
        public string NoContainer { get; set; }
        public string Quantity { get; set; }
        public string Sj1 { get; set; }
        public string Sj2 { get; set; }
        public string Sj3 { get; set; }
        public string Sj4 { get; set; }
        public string Sj5 { get; set; }
        public string Sj6 { get; set; }
        public string Sj7 { get; set; }
        public string Sj8 { get; set; }
        public string Sj9 { get; set; }
        public string Sj10 { get; set; }
        public string Sj11 { get; set; }
        public string Sj12 { get; set; }
        public string Sj13 { get; set; }
        public string Sj14 { get; set; }
        public string Sj15 { get; set; }
        public string Sj16 { get; set; }
        public string Sj17 { get; set; }
        public string Sj18 { get; set; }
        public string Sj19 { get; set; }
        public string Sj20 { get; set; }
        public string Sj21 { get; set; }
        public string Sj22 { get; set; }
        public string Sj23 { get; set; }
        public string Sj24 { get; set; }
        public string Sj25 { get; set; }
        public DateTime? TerimaToko { get; set; }
        public string Keterangan { get; set; }
        public string NoBA { get; set; }
        public int CustomerTransDetailDeleted { get; set; }

        //model field for view
        public string TypeName { get; set; }
        public string VesselName { get; set; }
        public string OriginName { get; set; }
        public string DestinationName { get; set; }
        public string ConditionName { get; set; }
        public string WarehouseName { get; set; }
        public string RecipientName { get; set; }
    }
}

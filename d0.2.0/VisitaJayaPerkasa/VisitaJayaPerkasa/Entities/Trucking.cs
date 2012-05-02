using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class Trucking
    {
        public Guid ID { get; set; }
        public Guid SupplierID { get; set; }
        public string TruckNo { get; set; }
    }
}

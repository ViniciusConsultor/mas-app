using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class Vessel
    {
        public Guid ID { get; set; }
        public string VesselCode { get; set; }
        public string VesselName { get; set; }
        public int Deleted { get; set; }
    }
}

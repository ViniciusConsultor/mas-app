using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class PelayaranDetail : Pelayaran
    {
        public Guid PelayaranDetailID { get; set; }
        public string VesselCode { get; set; }
        public string VesselName { get; set; }
        public int StatusPinjaman { get; set; }
        public string NamaStatusPinjaman { get; set; }
        public int PelayaranDetailDeleted { get; set; }
    }
}

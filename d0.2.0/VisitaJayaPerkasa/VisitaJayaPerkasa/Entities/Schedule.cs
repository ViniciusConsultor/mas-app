using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class Schedule
    {
        public Guid ID { get; set; }
        public Guid berangkat { get; set; }
        public Guid tujuan { get; set; }
        public Guid pelayaranID { get; set; }
        public Guid vesselID { get; set; }
        public DateTime etd { get; set; }
        public DateTime tglclosing { get; set; }
        public string voy { get; set; }
        public string keterangan { get; set; }
        public string ro { get; set; }


        //model for view
        public string berangkatTujuan { get; set; }
        public string namaPelayaran { get; set; }
        public string namaKapal { get; set; }

    }

}

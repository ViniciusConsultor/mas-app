using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class Schedule
    {
        public Guid ID { get; set; }
        public Guid tujuan { get; set; }

        public DateTime etd { get; set; }
        public DateTime? td { get; set; }
        public DateTime? eta { get; set; }
        public DateTime? ta { get; set; }
        public DateTime? unLoading { get; set; }
        public DateTime tglclosing { get; set; }

        public int ro_begin_20 { get; set; }
        public int ro_begin_40 { get; set; }
        public int ro_end_40 { get; set; }
        public int ro_end_20 { get; set; }

        public string voy { get; set; }
        public string keterangan { get; set; }
        public string vesselCode { get; set; }
        
        //model for view
        public string berangkatTujuan { get; set; }
        public string namaKapal { get; set; }
        public string namaPelayaran { get; set; }

    }

}

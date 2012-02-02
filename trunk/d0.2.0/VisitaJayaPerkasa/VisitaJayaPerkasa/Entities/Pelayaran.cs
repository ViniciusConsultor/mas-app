using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class Pelayaran
    {
        public Guid ID { get; set; }
        public Guid supplierID { get; set; }
        public int Deleted { get; set; }

        public string supplierName { get; set; }
    }
}

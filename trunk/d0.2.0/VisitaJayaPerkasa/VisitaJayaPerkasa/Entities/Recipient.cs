using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class Recipient
    {
        public Guid ID { get; set; }
        public Guid SupplierID { get; set; }
        public string Name { get; set; }
        public int Deleted { get; set; }

        //Field for view
        public string SupplierName { get; set; }
    }
}

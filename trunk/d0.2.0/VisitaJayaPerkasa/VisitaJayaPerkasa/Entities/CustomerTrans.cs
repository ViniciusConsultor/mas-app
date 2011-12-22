using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class CustomerTrans
    {
        public Guid CustomerTransID { get; set; }
        public Guid CustomerID { get; set; }
        public string CustomerName { get; set; }
        public DateTime? TransDate { get; set; }
        public int? Deleted { get; set; }
    }
}

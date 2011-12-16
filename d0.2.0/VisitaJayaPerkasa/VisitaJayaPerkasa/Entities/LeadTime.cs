using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class LeadTime
    {
        public Guid ID { get; set; }
        public Guid CityID { get; set; }
        public int Days { get; set; }
        public int Deleted { get; set; }

        //field for display in view
        public string CityName { get; set; }
    }
}

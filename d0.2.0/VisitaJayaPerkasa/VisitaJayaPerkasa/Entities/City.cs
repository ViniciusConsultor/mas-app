using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class City
    {
        public Guid ID { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public int Days { get; set; }
        public int Deleted { get; set; }
    }
}

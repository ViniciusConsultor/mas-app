using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class Condition
    {
        public Guid ID { get; set; }
        public string ConditionCode { get; set; }
        public string ConditionName { get; set; }
        public int Deleted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class TypeCont
    {
        public Guid ID { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public int Deleted { get; set; }
    }
}

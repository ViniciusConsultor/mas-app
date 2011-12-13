using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class Role
    {
        public Guid ID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int Deleted { get; set; }
    }
}

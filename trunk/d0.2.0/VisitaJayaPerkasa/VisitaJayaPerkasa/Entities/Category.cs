using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class Category
    {
        public Guid ID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public int Deleted { get; set; }
    }
}

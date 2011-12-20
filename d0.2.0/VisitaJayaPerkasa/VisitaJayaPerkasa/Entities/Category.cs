using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class Category
    {
        public Guid ID { get; set; }
        public string CategoryCode { set; get; }
        public string CategoryName { set; get; }
        public int Deleted { get; set; }
    }
    
}

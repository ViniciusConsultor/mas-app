using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class CustomerDetail : Customer
    {
        public Guid CustomerDetailID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomerDetailAddress { get; set; }
        public string CustomerDetailPhone { get; set; }
        public string CustomerDetailMobilePhone { get; set; }
        public int CustomerDetailDeleted { get; set; }
    }
}

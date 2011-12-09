using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisitaJayaPerkasa.Mvc.Models
{
    public class SupplierDetailModel
    {
        public Guid SupplierDetailId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string MobilePhone { get; set; }

        public int Deleted { get; set; }
    }
}
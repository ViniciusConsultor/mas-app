using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Business.Entities
{
    public class Supplier
    {
        public virtual Guid Id { get; set; }
        public virtual Guid CategoryId { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }
}

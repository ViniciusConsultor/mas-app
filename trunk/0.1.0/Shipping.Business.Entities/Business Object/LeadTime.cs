using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Business.Entities
{
    public class LeadTime
    {
        public virtual Guid Id { get; set; }
        public int Days { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Business.Entities
{
    public class Condition
    {
        public virtual Guid Id { get; set; }
        public string ConditionCode { get; set; }
        public string ConditionName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Business.Entities
{
    public class TypeCont
    {
        public virtual Guid Id { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
    }
}

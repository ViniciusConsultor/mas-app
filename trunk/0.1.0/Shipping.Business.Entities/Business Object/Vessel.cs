using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Business.Entities
{
    public class Vessel
    {
        public virtual Guid Id { get; set; }
        public string VesselName { get; set; }
    }
}

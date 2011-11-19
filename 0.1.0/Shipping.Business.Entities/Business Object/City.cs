using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Business.Entities
{
    public class City
    {
        public virtual Guid Id { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
    }
}

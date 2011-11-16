using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Business.Entities.Business_Object
{
    class Condition
    {
        Guid condition_id { get; set; }
        string condition_code { get; set; }
        string condition_name { get; set; }
    }
}

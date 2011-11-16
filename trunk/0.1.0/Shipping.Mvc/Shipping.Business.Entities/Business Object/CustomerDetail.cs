using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Business.Entities.Business_Object
{
    class CustomerDetail : Customer
    {
        Guid customer_detail_id { get; set; }
        string first_name { get; set; }
        string last_name { get; set; }
        string address { get; set; }
        string phone { get; set; }
        string mobile_phone { get; set; }
    }
}

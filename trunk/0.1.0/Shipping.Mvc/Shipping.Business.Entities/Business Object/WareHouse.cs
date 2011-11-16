using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Business.Entities.Business_Object
{
    class WareHouse
    {
        Guid stuffing_place_id { get; set; }
        string address { get; set; }
        string phone { get; set; }
        string fax { get; set; }
        string email { get; set; }
        string contact_person { get; set; }
    }
}

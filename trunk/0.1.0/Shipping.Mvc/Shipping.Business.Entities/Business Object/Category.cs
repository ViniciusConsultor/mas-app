using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Business.Entities.Business_Object
{
    class Category
    {
        Guid category_id { set; get; }
        string category_code { set; get; }
        string category_name { set; get; }
    }
}

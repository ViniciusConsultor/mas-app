using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Business.Entities
{
    public class Category
    {
        public virtual Guid Id { set; get; }
        public string CategoryCode { set; get; }
        public string CategoryName { set; get; }
    }
}

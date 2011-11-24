using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Shipping.Business.Entities.Collections;

namespace Shipping.Mvc.Models.Supplier
{
    public class SupplierIndexModel
    {
        public SupplierCollection Suppliers { get; set; }
    }
}
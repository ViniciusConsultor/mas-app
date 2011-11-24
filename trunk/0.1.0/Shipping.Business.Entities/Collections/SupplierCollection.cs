using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Shipping.Business.Entities.Collections
{
    [XmlRoot("Suppliers")]
    public class SupplierCollection : List<Supplier>
    {}
}

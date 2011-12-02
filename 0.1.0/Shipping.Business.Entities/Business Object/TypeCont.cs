using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Shipping.Business.Entities
{
    [Table(Name="Type_Cont")]
    public class TypeCont
    {
        [Column(Name="type_code", IsPrimaryKey=true)]
        public string TypeCode { get; set; }

        [Column(Name="type_name")]
        public string TypeName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Shipping.Business.Entities
{
    [Table(Name="Condition")]
    public class Condition
    { 
        [Column(Name="condition_code", IsPrimaryKey=true)]
        public string ConditionCode { get; set; }

        [Column(Name="condition_name")]
        public string ConditionName { get; set; }
    }
}

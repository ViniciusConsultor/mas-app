using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Shipping.Business.Entities
{
    [Table(Name="Lead_Time")]
    public class LeadTime
    {
        [Column(Name = "city_code", IsPrimaryKey=true)]
        public string CityCode { get; set; }

        [Column(Name="Days")]
        public int Days { get; set; }
    }
}

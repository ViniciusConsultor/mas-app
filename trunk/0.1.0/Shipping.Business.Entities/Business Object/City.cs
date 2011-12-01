using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Shipping.Business.Entities
{
    [Table(Name="City")]
    public class City
    {

        [Column(Name="city_code", IsPrimaryKey=true)]
        public string CityCode { get; set; }

        [Column(Name="city_name")]
        public string CityName { get; set; }
    }
}

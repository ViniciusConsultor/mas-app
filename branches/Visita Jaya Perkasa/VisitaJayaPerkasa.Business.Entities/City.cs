using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PetaPoco;

namespace VisitaJayaPerkasa.Business.Entities
{
    [TableName("CITY")]
    [PrimaryKey("city_code", autoIncrement = false)]
    public class City
    {
        [Column("city_code")]
        public string CityCode { get; set; }
       
        [Column("city_name")]
        public string CityName { get; set; }

        [Column("deleted")]
        public int Deleted { get; set; }
    }
}

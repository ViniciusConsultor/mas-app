using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PetaPoco;

namespace VisitaJayaPerkasa.Business.Entities
{
    [TableName("LEAD_TIME")]
    [PrimaryKey("city_code", autoIncrement = false)]
    public class LeadTime
    {
        [Column("city_code")]
        public string CityCode { get; set; }

        [Column("days")]
        public int Days { get; set; }
    }
}
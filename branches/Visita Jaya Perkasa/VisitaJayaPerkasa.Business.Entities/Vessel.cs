using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace VisitaJayaPerkasa.Business.Entities
{
     [TableName("VESSEL")]
    [PrimaryKey("vessel_code", autoIncrement = false)]
    public class Vessel
    {
        [Column("vessel_code")]
        public string VesselCode { get; set; }

        [Column("vessel_name")]
        public string VesselName { get; set; }

        [Column("deleted")]
        public int Deleted { get; set; }
    }
}

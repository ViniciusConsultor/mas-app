using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PetaPoco;

namespace VisitaJayaPerkasa.Business.Entities
{
    [TableName("CONDITION")]
    [PrimaryKey("condition_code", autoIncrement = false)]
    public class Condition
    {
        [Column("condition_code")]
        public string ConditionCode { get; set; }

        [Column("condition_name")]
        public string ConditionName { get; set; }

        [Column("deleted")]
        public int Deleted { get; set; }
    }
}

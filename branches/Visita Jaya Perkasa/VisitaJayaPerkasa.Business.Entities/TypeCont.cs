using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PetaPoco;

namespace VisitaJayaPerkasa.Business.Entities
{
    [TableName("TYPE_CONT")]
    [PrimaryKey("type_code", autoIncrement = false)]
    public class TypeCont
    {
        [Column("type_code")]
        public string TypeCode { get; set; }

        [Column("type_name")]
        public string TypeName { get; set; }

        [Column("deleted")]
        public int Deleted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace VisitaJayaPerkasa.Business.Entities
{
    [TableName("CATEGORY")]
    [PrimaryKey("category_code", autoIncrement = false)]
    public class Category
    {
        [Column("category_code")]
        public string CategoryCode { set; get; }

        [Column("category_name")]
        public string CategoryName { set; get; }

        [Column("deleted")]
        public int Deleted { get; set; }
    }
    
}

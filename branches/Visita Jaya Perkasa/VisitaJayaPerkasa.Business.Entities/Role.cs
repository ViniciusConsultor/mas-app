using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PetaPoco;

namespace VisitaJayaPerkasa.Business.Entities
{
    [TableName("ROLE")]
    [PrimaryKey("role_name", autoIncrement = false)]
    public class Role
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Column("role_name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Column("description")]
        public string Description { get; set; }

        [Column("deleted")]
        public int Deleted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PetaPoco;

namespace VisitaJayaPerkasa.Business.Entities
{
    [TableName("ROLE")]
    [PrimaryKey("role_id", autoIncrement = false)]
    public class Role
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [Column("role_id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Column("description")]
        public string Description { get; set; }
    }
}

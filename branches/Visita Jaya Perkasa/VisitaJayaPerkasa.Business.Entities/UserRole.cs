using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace VisitaJayaPerkasa.Business.Entities
{
    [TableName("USER_ROLE")]
    [PrimaryKey("user_role_id", autoIncrement = false)]
    public class UserRole
    {
        [Column("user_role_id")]
        public Guid UserRoleId { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("role_name")]
        public string RoleName { get; set; }

        [Column("deleted")]
        public int Deleted { get; set; }
    }
}

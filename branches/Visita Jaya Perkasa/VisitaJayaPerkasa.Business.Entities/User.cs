using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PetaPoco;

namespace VisitaJayaPerkasa.Business.Entities
{
    [TableName("USER")]
    [PrimaryKey("person_id", autoIncrement = false)]
    public class User
    {
        [Column("person_id")]
        public Guid Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("salt")]
        public string Salt { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("user_id")]
        public string middle_initial { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone")]
        public string PhoneNumber { get; set; }

        [Column("mobile_phone_number")]
        public string MobilePhoneNumber { get; set; }

        [Column("nik")]
        public string Nik { get; set; }
        
        [Column("address")]
        public string address { get; set; }
        
        [Column("date_Of_birth")]
        public string DateOfBirth { get; set; }
        
        [Column("martial_status")]
        public string MartialStatus { get; set; }
        
        [Column("gender")]
        public string Gender { get; set; }

        public override string ToString()
        {
            return this.FirstName + ", " + this.LastName;
        }
    }
}

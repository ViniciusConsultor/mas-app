using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class User
    {
        public Guid PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordHint { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public DateTime PasswordChangeDate { get; set; }
        public string MobilePhoneNumber { get; set; }
        public int Deleted { get; set; }

        public Role RoleObj { get; set; }
    }
}

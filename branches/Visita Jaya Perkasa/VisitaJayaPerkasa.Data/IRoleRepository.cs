using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Data
{
    public interface IRoleRepository
    {
        void SaveRole(Role role);
        void DeleteRole(string ID);
        IEnumerable<Role> GetListRole();
        Role GetRoleByID(string ID);
    }
}

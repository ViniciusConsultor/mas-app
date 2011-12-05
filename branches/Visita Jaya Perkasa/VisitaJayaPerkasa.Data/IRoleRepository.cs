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
        void DeleteRole(Guid ID);
        IEnumerable<Role> GetListRole();
        Role GetRoleByID(Guid ID);
    }
}

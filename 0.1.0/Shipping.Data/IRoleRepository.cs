using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Data
{
    public interface IRoleRepository
    {
        bool CreateRole(Shipping.Business.Entities.Role role);
        bool EditRole(Shipping.Business.Entities.Role role);
        bool DeleteRole(string ID);
        IEnumerable<Shipping.Business.Entities.Role> GetListRole();
        Shipping.Business.Entities.Role GetRoleByID(string ID);
    }
}

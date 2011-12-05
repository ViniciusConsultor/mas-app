using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Services;
using VisitaJayaPerkasa.Data;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Business.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _RoleRepository;
        public RoleService(IRoleRepository roleRepository) {
            _RoleRepository = roleRepository;
        }

        public void SaveRole(Role role)
        {
           _RoleRepository.SaveRole(role);
        }
        public void DeleteRole(Guid ID)
        {
            _RoleRepository.DeleteRole(ID);
        }

        public IEnumerable<Role> GetListRole()
        {
            return _RoleRepository.GetListRole();
        }

        public Role GetRoleByID(Guid ID)
        {
            return _RoleRepository.GetRoleByID(ID);
        }
    }
}

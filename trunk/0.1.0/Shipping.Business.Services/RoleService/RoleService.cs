using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Business.Services.RoleService;
using Shipping.Data;

namespace Shipping.Business.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _RoleRepository;
        public RoleService(IRoleRepository roleRepository) {
            _RoleRepository = roleRepository;
        }

        public bool CreateRole(Entities.Role role)
        {
            return _RoleRepository.CreateRole(role);
        }

        public bool EditRole(Entities.Role role)
        {
            return _RoleRepository.EditRole(role);
        }

        public bool DeleteRole(string ID)
        {
            return _RoleRepository.DeleteRole(ID);
        }

        public IEnumerable<Entities.Role> GetListRole()
        {
            return _RoleRepository.GetListRole();
        }

        public Entities.Role GetRoleByID(string ID)
        {
            return _RoleRepository.GetRoleByID(ID);
        }
    }
}

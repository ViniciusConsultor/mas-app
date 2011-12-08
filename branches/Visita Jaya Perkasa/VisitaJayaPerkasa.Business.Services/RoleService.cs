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
        public void DeleteRole(string ID)
        {
            _RoleRepository.DeleteRole(ID);
        }

        public IEnumerable<Role> GetListRole()
        {
            return _RoleRepository.GetListRole();
        }

        public Role GetRoleByID(string ID)
        {
            return _RoleRepository.GetRoleByID(ID);
        }


        public IEnumerable<Role> GetListRoleBySearch(string searchWord)
        {
            return _RoleRepository.GetListRoleBySearch(searchWord);
        }
    }
}

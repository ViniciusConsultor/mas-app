﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Data.Sql
{
    public class RoleRepository : IRoleRepository
    {
        private readonly string _mainConnectionString;
        public RoleRepository(string mainConnectionString)
        {
            _mainConnectionString = mainConnectionString;
        }

        public void SaveRole(Role role)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            using (var scope = repo.GetTransaction())
            {
                if (GetRoleByID(role.Name) == null)
                {
                    //Create new
                    repo.Insert(role);
                }
                else
                {
                    //Update it

                    repo.Update("ROLE", "role_name", role);
                }

                scope.Complete();
            }

            repo.CloseSharedConnection();
        }

        public void DeleteRole(string name)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();
            Role role = GetRoleByID(name);
            repo.Delete("ROLE", "role_id", role);
            repo.CloseSharedConnection();
        }

        public IEnumerable<Role> GetListRole()
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            List<Role> roles = repo.Fetch<Role>("SELECT * FROM [ROLE]").ToList<Role>();

            repo.CloseSharedConnection();

            return roles;
        }

        public Role GetRoleByID(string ID)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            Role role = repo.SingleOrDefault<Role>("SELECT * FROM Role WHERE role_name=@0", ID);

            repo.CloseSharedConnection();

            return role;
        }

    }
}

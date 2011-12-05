using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PetaPoco;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Data.Sql
{
    public class UserRepository : IUserRepository
    {
        private string _mainConnectionString;

        public UserRepository(string mainConnectionString)
        {
            _mainConnectionString = mainConnectionString;
        }


        #region IUserRepository Members

        public User GetUserByUsername(string username)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            User user = repo.SingleOrDefault<User>("SELECT * FROM [USER] WHERE username=@0", username);

            repo.CloseSharedConnection();

            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            List<User> users = repo.Fetch<User>("SELECT * FROM [USER]").ToList<User>();

            repo.CloseSharedConnection();

            return users;
        }

        public List<Role> GetRolesByUserId(Guid userId)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            List<Role> roles = repo.Fetch<Role>(@"SELECT DISTINCT r.role_id, r.name, r.[description] FROM [ROLE] r INNER JOIN USER_ROLE ura ON r.role_id = ura.role_id WHERE ura.[user_id] = @0 ORDER BY	r.name, r.[description]", userId).ToList<Role>();

            repo.CloseSharedConnection();

            return roles;
        }

        #endregion

        #region IUserRepository Members

        public void SaveUser(User user)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            using (var scope = repo.GetTransaction())
            {
                if (GetUserByUsername(user.Username) == null)
                {
                    //Create new
                    repo.Insert(user);
                    //repo.Insert(userRole);
                }
                else
                {
                    //Update it

                    repo.Update("User", "person_id", user);
                    //repo.Update("USER_ROLE", "user_role_id", userRole);
                }

                scope.Complete();
            }

            repo.CloseSharedConnection();
        }

        #endregion
    }
}

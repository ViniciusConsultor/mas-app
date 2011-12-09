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

        public User GetUser(Guid id)
        {

            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            User user = repo.SingleOrDefault<User>("SELECT * FROM [USER] WHERE person_id=@0", id);

            repo.CloseSharedConnection();

            return user;
        }

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

            List<User> users = repo.Fetch<User>("SELECT * FROM [USER] WHERE (deleted is null OR deleted = '0')").ToList<User>();

            repo.CloseSharedConnection();

            return users;
        }

        public IEnumerable<User> GetAllDeletedUsers()
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            List<User> users = repo.Fetch<User>("SELECT * FROM [USER] WHERE deleted = '1'").ToList<User>();

            repo.CloseSharedConnection();

            return users;
        }

        public List<Role> GetRolesByUserId(Guid userId)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            List<Role> roles = repo.Fetch<Role>(@"SELECT DISTINCT r.role_name, r.[description] FROM [ROLE] r INNER JOIN USER_ROLE ura ON r.role_name = ura.role_name WHERE ura.[user_id] = @0 AND (r.deleted is null OR r.deleted = '0') AND (ura.deleted is null OR ura.deleted = '0') ORDER BY	r.role_name, r.[description]", userId).ToList<Role>();

            repo.CloseSharedConnection();

            return roles;
        }

        #endregion

        #region IUserRepository Members

        public void SaveUser(User user, List<UserRole> userRoles)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            using (var scope = repo.GetTransaction())
            {
                if (GetUserByUsername(user.Username) == null)
                {
                    //Create new
                    repo.Insert(user);
                    foreach(UserRole userRole in userRoles)
                        repo.Insert(userRole);
                }
                else
                {
                    //Update it

                    repo.Update("User", "person_id", user);
                    foreach (UserRole userRole in userRoles)
                        repo.Update("USER_ROLE", "user_role_id", userRole);
                }

                scope.Complete();
            }

            repo.CloseSharedConnection();
        }

        public void DeleteUser(Guid Id)
        {
            User user = GetUser(Id);

            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();

            //delete user roles
            List<UserRole> userRoles = repo.Fetch<UserRole>(@"SELECT DISTINCT * FROM USER_ROLE WHERE [user_id] = @0 AND (deleted is null OR deleted = '0')", Id).ToList<UserRole>();
            foreach (UserRole userRole in userRoles)
            {
                userRole.Deleted = 1;
                repo.Update("USER_ROLE", "user_role_id", userRole);
            }
            //delete user
            user.Deleted = 1;
            repo.Update("User", "person_id", user);
            
            repo.CloseSharedConnection();
        }

        public void UndeleteUser(Guid Id)
        {
            User user = GetUser(Id);

            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();

            //delete user roles
            List<UserRole> userRoles = repo.Fetch<UserRole>(@"SELECT DISTINCT * FROM USER_ROLE WHERE [user_id] = @0 AND deleted = '1'", Id).ToList<UserRole>();
            foreach (UserRole userRole in userRoles)
            {
                userRole.Deleted = 0;
                repo.Update("USER_ROLE", "user_role_id", userRole);
            }
            //delete user
            user.Deleted = 0;
            repo.Update("User", "person_id", user);

            repo.CloseSharedConnection();
        }

        #endregion
    }
}

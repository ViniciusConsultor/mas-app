using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Data
{
    public interface IUserRepository
    {
        void SaveUser(User user, List<UserRole> userRoles);

        User GetUser(Guid id);

        User GetUserByUsername(string username);

        IEnumerable<User> GetAllUsers();

        IEnumerable<User> GetAllDeletedUsers();

        List<Role> GetRolesByUserId(Guid userId);

        void DeleteUser(Guid Id);

        void UndeleteUser(Guid Id);
    }
}

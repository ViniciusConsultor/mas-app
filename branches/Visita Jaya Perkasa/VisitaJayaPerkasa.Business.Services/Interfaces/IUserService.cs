using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Business.Services
{
    public interface IUserService
    {
        bool ValidateUser(string username, string password);

        User GetUserByUsername(string username);

        List<Role> GetRolesByUserId(Guid userId);

        IEnumerable<User> GetAllUsers();

        void SaveUser(User user);

        void DeleteUser(Guid Id);
    }
}
